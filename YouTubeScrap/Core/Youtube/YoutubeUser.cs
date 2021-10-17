using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core.ReverseEngineer;
using YouTubeScrap.Handlers;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Core.Youtube
{
    //TODO: After making the user, call the API to get the user details. For now we can use the user to get logged in responses from the YouTube API.
    //TODO: Write the user/cookie functionality out in Obsidian or something to make the implementation more clear.
    //TODO: Make a system to save user to disk in binary or hashed JSON/binary, and maybe add a password/hash protection to the file.
    //TODO: Reimplement the 'DataManager' it has more user specific data, and with the 'NetworkHandler' reimplemented it will make more sense set the data to the user class.
    public class YoutubeUser
    {
        public CookieContainer UserCookieContainer
        {
            get => _userCookieContainer;
            set
            {
                _userCookieContainer = value;
                ValidateCookies();
            }
        }
        public WebProxy UserProxy
        {
            get => _userProxy;
            set
            {
                _userProxy = value;
                _network = new NetworkHandler(this);
            }
        }
        public UserData UserData;
        public UserSettings UserSettings;
        public ClientData ClientData => _clientData;
        public NetworkHandler NetworkHandler => _network;
        public bool HasLogCookies = false;

        private string PathToSave => Path.Combine(SettingsManager.Settings.UserStoragePath, $"user_{UserData.UserID}");
        private NetworkHandler _network;
        private CookieContainer _userCookieContainer;
        private WebProxy _userProxy;
        private Cookie userSAPISID;
        private readonly BinaryFormatter _bFormatter;
        private ClientData _clientData;
        private const string ClientState = "{\"CLIENT_CANARY_STATE\":";
        private const string ResponseContext = "{\"responseContext\":";
        private readonly Regex _jsonRegex = new Regex(@"\{(?:[^\{\}]|(?<o>\{)|(?<-o>\}))+(?(o)(?!))\}");
        private JObject _initialResponse;

        /// <summary>
        /// Setup a user, for browsing YouTube. If no cookies are given and/or the config has no default to load account, then we will setup a default user that is NOT logged in, and will be temporary cached to disk/memory.
        /// The default user(Not logged in) will be used if there is no user cookies or login is given and for anonymous browsing. Cookies will be temporary, and or will be deleted when the system/app restarts.
        /// </summary>
        /// <param name="cookieJar">CookieContainer with the required cookies to receive user data, and to perform user actions.</param>
        /// <param name="proxy">The proxy to use for the current user.</param>
        public YoutubeUser(CookieContainer cookieJar = null, WebProxy proxy = null)
        {
            //TODO: Check the main config for default user to load on startup!
            _bFormatter = new BinaryFormatter();
            _userProxy = proxy;
            _userCookieContainer = cookieJar ?? new CookieContainer();
            ValidateCookies();
            _network = new NetworkHandler(this);
            
            Task.Run(MakeInitRequest);
        }
        private void ValidateCookies()
        {
            if (TryGetCookie("SAPISID", out Cookie cookieOut))
            {
                userSAPISID = cookieOut;
                HasLogCookies = true;
            }
            else
                Trace.WriteLine("Could not acquire the SAPISID/__Secure-3PAPISID cookie! User is unable to login!");
        }
        public void SaveUser()
        {
            try
            {
                using (Stream writeStream = File.Create(Path.Combine(PathToSave, "user_data.ytudata")))
                {
                    _bFormatter.Serialize(writeStream, UserData);
                }
                using (Stream writeStream = File.Create(Path.Combine(PathToSave, "user_cookies.ytucookies")))
                {
                    _bFormatter.Serialize(writeStream, UserCookieContainer);
                }
                using (StreamWriter writer = new StreamWriter(Path.Combine(PathToSave, "user_settings.json")))
                {
                    writer.Write(JsonConvert.SerializeObject(UserSettings));
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Exception while writing user data to disk!\nException: {e}");
            }
        }
        public static CookieContainer ReadCookies()
        {
            using (Stream readStream = File.Open(Path.Combine(Directory.GetCurrentDirectory(), "user_cookies.yt_cookies"), FileMode.Open))
            {
                try
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    return (CookieContainer)bFormatter.Deserialize(readStream);
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"Exception while reading cookies from disk!: {e}");
                }
            }
            return null;
        }
        // Needed to implement this function because the default CookieContainer class does not have this simple function, to get all the cookies without passing the domain URI! Why?!
        public CookieCollection GetAllCookies()
        {
            Hashtable domainTable = (Hashtable)_userCookieContainer.GetType().GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_userCookieContainer);
            if (domainTable == null)
                return null;
            CookieCollection cookieCollection = new CookieCollection();
            foreach (DictionaryEntry domain in domainTable)
            {
                SortedList cookieList = (SortedList)domain.Value.GetType().GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(domain.Value);
                if (cookieList == null)
                    continue;
                foreach (DictionaryEntry cookieColl in cookieList)
                {
                    cookieCollection.Add((CookieCollection)cookieColl.Value);
                }
            }
            return cookieCollection;
        }
        public bool TryGetCookie(string name, out Cookie cookieOut)
        {
            cookieOut = null;
            if (string.IsNullOrEmpty(name)) return false;
            CookieCollection cookieCol = GetAllCookies();
            cookieOut = cookieCol[name];
            return cookieOut != null;
        }
        public AuthenticationHeaderValue GenerateAuthentication()
        {
            return UserAuthentication.GetSapisidHashHeader(userSAPISID.Value);
        }

        private void MakeInitRequest()
        {
            ApiRequest request = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Home, this);
            HttpResponse response = NetworkHandler.MakeApiRequestAsync(request, true).Result;
            ExtractDataFromHtml(response.ResponseString);
        }
        //TODO: Function need to be rewritten, it is not really elegant.
        private void ExtractDataFromHtml(string htmlData)
        {
            if (htmlData.IsNullEmpty())
                return;
            MatchCollection regexMatch = _jsonRegex.Matches(htmlData);
            bool partFound = false;
            JObject responseContext = null;
            foreach (Match match in regexMatch)
            {
                if (match.Value.Contains(ResponseContext))
                {
                    responseContext =
                        JsonConvert.DeserializeObject<JObject>(match.Value, new JsonDeserializeConverter());
                    if (partFound)
                        break;
                    partFound = true;
                }

                if (match.Value.Contains(ClientState))
                {
                    string searchValue = match.Value.Substring(match.Value.IndexOf(ClientState, StringComparison.Ordinal));
                    MatchCollection jsonMatch = _jsonRegex.Matches(searchValue);
                    foreach (Match json in jsonMatch)
                    {
                        if (json.Value.Contains(ClientState))
                        {
                            _clientData.ClientState = JObject.Parse(json.Value);
                            continue;
                        }
                        try
                        {
                            _clientData.LanguageDefinitions = JObject.Parse(json.Value);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    if (partFound)
                        break;
                    partFound = true;
                }
            }
            _initialResponse = responseContext;
        }
    }
    public struct UserData
    {
        public string UserID { get; set; } // User/Channel ID.
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
    }
    public struct UserSettings
    {
        // Will be populated when there a settings implemented!
    }
    public struct ClientData
    {
        public bool ContainsData => ClientState != null;
        public JObject ClientState { get; set; }
        public JObject LanguageDefinitions { get; set; }

        public string ApiKey => ClientState["INNERTUBE_API_KEY"]?.ToString();
        public string LoginUrl => ClientState["SIGNIN_URL"]?.ToString();
    }
}