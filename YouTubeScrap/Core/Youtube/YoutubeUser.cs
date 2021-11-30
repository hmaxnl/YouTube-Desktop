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
using YouTubeScrap.Data;
using YouTubeScrap.Handlers;

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

        private string PathToSave => Path.Combine(SettingsManager.Settings.UserStoragePath, $"user_{UserData.UserId}");
        private NetworkHandler _network;
        private CookieContainer _userCookieContainer;
        private WebProxy _userProxy;
        private Cookie userSAPISID;
        private readonly BinaryFormatter _bFormatter;
        private ClientData _clientData;
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
            /* While using the Path.Combine(Directory.GetCurrentPath(), "user_cookies.yt_cookies")
            the AXAML preview and avalonia designer (design mode) bugs out. It displays that it cannot find the file for some reason, and with a static path like now it cannot open the file.
            Still throws an error but the preview works!*/
            using (Stream readStream = File.Open("/home/max/Git/YouTube-Desktop/YouTubeGUI/bin/Debug/net5.0/user_cookies.yt_cookies", FileMode.Open))
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
        // Needed to implement this function because the default CookieContainer class does not have this simple function, to get all the cookies without passing the domain URI! Like why?!
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
        public async Task<ResponseMetadata> MakeInitRequest()
        {
            Trace.WriteLine("Making init request");
            
            ApiRequest homeRequest = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Home, this);
            var response = await NetworkHandler.MakeApiRequestAsync(homeRequest, true);
            
            var htmlExtract = HtmlHandler.ExtractFromHtml(response.ResponseString);
            _clientData = htmlExtract.ClientData;
            return JsonConvert.DeserializeObject<ResponseMetadata>(htmlExtract.Response.ToString());
        }
    }
    public struct UserData
    {
        public string UserId { get; set; } // User/Channel ID.
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