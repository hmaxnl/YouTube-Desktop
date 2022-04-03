using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using YouTubeScrap.Core.Exceptions;
using YouTubeScrap.Core.ReverseEngineer;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Innertube;
using YouTubeScrap.Handlers;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Core.Youtube
{
    /// <summary>
    /// Class for making requests to YouTube!
    /// Every user has different cookies to keep users separate, for 'non logged' in users there will be a new class created without cookies!
    /// Those CANNOT perform user specific actions like, subscribe, comment and more!
    /// </summary>
    public class YoutubeUser : IDisposable //TODO: Need to make a call to youtube after creation to get the cookies required. Those will be used to tell the sessions and users apart.
    {
        /// <summary>
        /// Setup a user, for browsing YouTube. If no cookies are given and/or the config has no default to load a user, then there will be a user setup that is NOT logged in, and will be temporary cached to disk/memory.
        /// Users created without loaded cookies are non logged in users, and can be used for 'anonymous' browsing youtube.
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
            // Wait for the initial response this contains some data needed to make further requests!
            InitialResponse().Wait();
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
        public ResponseMetadata InitialResponseMetadata { get; private set; }

        //==============================
        // Private internal properties
        //==============================
        private string PathToSave => Path.Combine(SettingsManager.Settings.UserStorePath, $"user_{UserData.UserId}");
        private string Cookie_YSC => TryGetCookie("YSC", out Cookie yscCookie) ? yscCookie.Value : String.Empty;
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
            //TODO: Implement default/last used user to load, from settings manager.
            using (Stream readStream = File.Open("cookiepath", FileMode.Open))
            {
                try
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    return (CookieContainer)bFormatter.Deserialize(readStream);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error while reading cookies from disk!");
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
            cookieOut = GetAllCookies()[name];
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
                Log.Error(e, "Error while writing user data to disk!");
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
        public async Task<ResponseMetadata> GetApiMetadataAsync(ApiRequestType apiCall, string query = null, string continuation = null, string id = null, object endpoint = null)
        {
            ApiRequest apiRequest = YoutubeApiManager.PrepareApiRequest(apiCall, YoutubeApiManager.BuildPrep(this, query, continuation, id, endpoint));
            var response = await NetworkHandler.MakeApiRequestAsync(apiRequest);
            JObject jsonData = null;
            switch (response.ContentType)
            {
                case ResponseContentType.HTML:
                {
                    var htmlExtraction = await HtmlHandler.ExtractFromHtmlAsync(response.ResponseString);
                    jsonData = htmlExtraction.Response;
                    if (htmlExtraction.ClientData != null)
                        _clientData = htmlExtraction.ClientData;
                    break;
                }
                case ResponseContentType.JSON:
                    jsonData = JsonConvert.DeserializeObject<JObject>(response.ResponseString,
                        new JsonDeserializeConverter());
                    /*!!! For debugging! !!!*/
                    /*if (apiCall == ApiRequestType.Guide)
                    {
                        string jsonGuide = File.ReadAllText("/run/media/max/DATA_3TB/Programming/JSON Responses/Guide/guide_logged_in.json");
                        jsonData = JsonConvert.DeserializeObject<JObject>(jsonGuide,
                            new JsonDeserializeConverter());
                    }
                    else
                    {
                        jsonData = JsonConvert.DeserializeObject<JObject>(response.ResponseString,
                            new JsonDeserializeConverter());
                    }*/
                    break;
            }

            if (jsonData == null) return null;
            return JsonConvert.DeserializeObject<ResponseMetadata>(jsonData.ToString());
        }
        public void Dispose() => _network.Dispose();
        
        /// <summary>
        /// Make a hash code from the 'YSC' cookie.
        /// </summary>
        /// <returns>The hash code from the 'YSC' cookie | If no 'YSC' cookies are available then return '0' zero</returns>
        public override int GetHashCode()
        {
            // Get the 'YSC' cookie to make a unique hash.
            return Cookie_YSC.IsNullEmpty() ? 0 : Cookie_YSC.GetHashCode();
        }

        /// <summary>
        /// Check if the classes are equal, comparing the 'YSC' cookies.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True is data's are the same.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not YoutubeUser user) return false;
            return user?.Cookie_YSC == Cookie_YSC;
        }
        
        //==============================
        // private functions
        //==============================
        private void ValidateCookies()
        {
            // Check the 'SAPISID' to validate if the user is 'logged in', and not expired.
            if (TryGetCookie("SAPISID", out Cookie sapisidCookie))
            {
                _userSapisid = sapisidCookie;
                HasLogCookies = !sapisidCookie.Expired;
            }
            else
                Log.Warning("Could not acquire the SAPISID/__Secure-3PAPISID cookie! User is unable to perform authenticated actions to the API!");
        }

        private async Task InitialResponse()
        {
            InitialResponseMetadata = await GetApiMetadataAsync(ApiRequestType.Home);
            if (ClientData == null)
            {
                Log.Fatal("Request for initial data failed!");
                throw new NullClientDataException("'ClientData' is null! Cannot build user missing required data!");
            }
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
}