using System;
using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Exceptions;
using YouTubeScrap.Core.ReverseEngineer;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Innertube;
using YouTubeScrap.Handlers;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap
{
    /// <summary>
    /// Class for making requests to YouTube!
    /// Every user has different cookies to keep users separate, for 'non logged' in users there will be a new class created without cookies!
    /// Those CANNOT perform user specific actions like, subscribe, comment and more!
    /// </summary>
    public class YoutubeUser : IDisposable
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
            _userProxy = proxy;
            _userCookieContainer = cookieJar ?? new CookieContainer();
            ValidateCookies();
            _network = new NetworkHandler(this);
            InitialRequestTask = Task.Run(InitialRequest);
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
        public ClientData ClientData => _clientData;
        public NetworkHandler NetworkHandler => _network;
        public bool HasLogCookies;
        public ResponseMetadata InitialResponseMetadata
        {
            get
            {
                // Wait for the task to finish fetching the data.
                InitialRequestTask.Wait();
                return _initialResponseMeta;
            }
            private set => _initialResponseMeta = value;
        }
        private ResponseMetadata _initialResponseMeta;

        //==============================
        // Private internal properties
        //==============================
        //private string PathToSave => Path.Combine(Manager.Properties.GetString("StoragePath"), "users", $"user_ID");
        private string CookieYsc => TryGetCookie("YSC", out Cookie yscCookie) ? yscCookie.Value : String.Empty;
        private NetworkHandler _network;
        private CookieContainer _userCookieContainer;
        private WebProxy _userProxy;
        private Cookie _userSapisid;
        private ClientData _clientData;
        public readonly Task InitialRequestTask;
        
        //==============================
        // Functions
        //==============================
        public CookieCollection GetAllCookies()
        {
            Hashtable domainTable = (Hashtable)_userCookieContainer.GetType().GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_userCookieContainer);
            if (domainTable == null)
                return null;
            CookieCollection cookieCollection = new CookieCollection();
            foreach (DictionaryEntry domain in domainTable)
            {
                SortedList cookieList = (SortedList)domain.Value?.GetType().GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(domain.Value);
                if (cookieList == null)
                    continue;
                foreach (DictionaryEntry cookieColl in cookieList)
                {
                    if (cookieColl.Value == null) continue;
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
        public AuthenticationHeaderValue GenerateAuthentication() => UserAuthentication.GetSapisidHashHeader(_userSapisid.Value);
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
                    break;
            }

            if (jsonData == null) return null;
            return JsonConvert.DeserializeObject<ResponseMetadata>(jsonData.ToString()) ?? new ResponseMetadata();
        }
        public void Dispose() => _network.Dispose();
        
        /// <summary>
        /// Make a hash code from the 'YSC' cookie.
        /// </summary>
        /// <returns>The hash code from the 'YSC' cookie | If no 'YSC' cookies are available then return '0' zero</returns>
        public override int GetHashCode() => CookieYsc.IsNullEmpty() ? 0 : CookieYsc.GetHashCode();

        /// <summary>
        /// Check if the classes are equal, comparing the 'YSC' cookies.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if data's are the same.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not YoutubeUser user) return false;
            return user.CookieYsc == CookieYsc;
        }
        
        //==============================
        // Private functions
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

        // Initial response for the user. With this we will obtain some data we need for further requests, API calls, etc.
        private async Task InitialRequest()
        {
            InitialResponseMetadata = await GetApiMetadataAsync(ApiRequestType.Home);
            if (ClientData == null)
            {
                Log.Fatal("Request for initial data failed!");
                throw new NullClientDataException("'ClientData' is null! Cannot create user missing required data!");
            }
        }
    }
}