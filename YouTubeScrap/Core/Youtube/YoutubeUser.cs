using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core.ReverseEngineer;
using YouTubeScrap.Data;
using YouTubeScrap.Handlers;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Core.Youtube
{
    //TODO: After making the user, call the API to get the user details. For now we can use the user to get logged in responses from the YouTube API.
    //TODO: Make a system to save user to disk in binary or hashed JSON/binary, and maybe add a password/hash protection to the file.
    //TODO: Reimplement the 'DataManager' it has more user specific data, and with the 'NetworkHandler' reimplemented it will make more sense set the data to the user class.
    public class YoutubeUser : IDisposable
    {
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
        
        //==============================
        // Public properties
        //==============================
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

        //==============================
        // Private internal properties
        //==============================
        private string PathToSave => Path.Combine(SettingsManager.Settings.UserStoragePath, $"user_{UserData.UserId}");
        private NetworkHandler _network;
        private CookieContainer _userCookieContainer;
        private WebProxy _userProxy;
        private Cookie _userSapisid;
        private readonly BinaryFormatter _bFormatter;
        private ClientData _clientData;
        
        //==============================
        // Functions
        //==============================
        public static CookieContainer ReadCookies()
        {
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
        public void SaveUser() //TODO: Need to be further implemented.
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
        public AuthenticationHeaderValue GenerateAuthentication()
        {
            return UserAuthentication.GetSapisidHashHeader(_userSapisid.Value);
        }
        /*public async Task<VideoDataSnippet> GetVideo(string videoId)
        {
            ApiRequest videoReq = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Video, this, null, null, videoId);
            var response = await NetworkHandler.MakeApiRequestAsync(videoReq);
            var htmlToJson = HtmlHandler.ExtractJsonFromHtml(response.ResponseString, HTMLExtractions.PlayerResponse, this);
            VideoDataSnippet vds = JsonConvert.DeserializeObject<VideoDataSnippet>(htmlToJson.ToString());
            return vds;
        }*/
        public async Task<ResponseMetadata> GetApiMetadataAsync(ApiRequestType apiCall)
        {
            ApiRequest apiRequest = YoutubeApiManager.PrepareApiRequest(apiCall, this);
            var response = await NetworkHandler.MakeApiRequestAsync(apiRequest);
            JObject jsonData = new JObject();
            switch (response.ContentType)
            {
                case ResponseContentType.HTML:
                {
                    var htmlExtraction = await HtmlHandler.ExtractFromHtmlAsync(response.ResponseString);
                    jsonData = htmlExtraction.Response;
                    _clientData = htmlExtraction.ClientData;
                    break;
                }
                case ResponseContentType.JSON:
                    jsonData = JsonConvert.DeserializeObject<JObject>(response.ResponseString,
                        new JsonDeserializeConverter());
                    break;
            }
            return JsonConvert.DeserializeObject<ResponseMetadata>(jsonData.ToString());
        }
        public void Dispose() => _network.Dispose();
        
        //==============================
        // private functions
        //==============================
        private void ValidateCookies()
        {
            if (TryGetCookie("SAPISID", out Cookie cookieOut))
            {
                _userSapisid = cookieOut;
                HasLogCookies = true;
            }
            else
                Trace.WriteLine("Could not acquire the SAPISID/__Secure-3PAPISID cookie! User is unable to login!");
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