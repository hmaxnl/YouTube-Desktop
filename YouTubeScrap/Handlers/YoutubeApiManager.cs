using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;
using HttpMethod = System.Net.Http.HttpMethod;

namespace YouTubeScrap.Handlers
{
    public static class YoutubeApiManager
    {
        private static readonly string[] endpointMaps = new[] { "commandEndpointMap:", "signalEndpointMap:", "continuationEndpointMap:", "watchEndpointMap:", "reelWatchEndpointMap:" };
        public static ApiRequest PrepareApiRequest(ApiRequestType requestType, YoutubeUser user, string query = null, string continuation = null, string id = null)
        {
            ApiRequest apiRequest = new ApiRequest();
            switch (requestType)
            {
                case ApiRequestType.Account:
                    apiRequest.Payload = DefaultRequired(user);
                    apiRequest.ApiUrl = $"/youtubei/v1/account/account_menu?key={user.ClientData.ApiKey}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = true;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Search:
                    if (query.IsNullEmpty())
                        break;
                    apiRequest.Payload = DefaultRequired(user);
                    apiRequest.Payload.Add("query", query);
                    apiRequest.Payload.Add("continuation", continuation);
                    apiRequest.ApiUrl = $"/youtubei/v1/search?key={user.ClientData.ApiKey}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Guide:
                    apiRequest.Payload = DefaultRequired(user);
                    apiRequest.ApiUrl = $"/youtubei/v1/guide?key={user.ClientData.ApiKey}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Playlist:
                    break;
                case ApiRequestType.Home:
                    apiRequest.Method = HttpMethod.Get;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.HTML;
                    break;
                case ApiRequestType.HomeBrowse:
                    apiRequest.Payload = DefaultRequired(user);
                    apiRequest.Payload.Add("continuation", continuation);
                    apiRequest.Payload.Add("browseId", "FEwhat_to_watch");
                    apiRequest.ApiUrl = $"/youtubei/v1/browse?key={user.ClientData.ApiKey}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Channel:
                    break;
                case ApiRequestType.Video:
                    apiRequest.ApiUrl = $"/watch?v={id}";// HTML
                    apiRequest.Method = HttpMethod.Get;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.HTML;
                    break;
            }
            return apiRequest;
        }
        private static JObject DefaultRequired(YoutubeUser ytUser)
        {
            JObject innertubeContext = ytUser.ClientData.ClientState["INNERTUBE_CONTEXT"]?.Value<JObject>();
            if (innertubeContext == null)
                return null;
            innertubeContext.Remove("clickTracking");
            JObject context = new JObject();
            context.Add("context", innertubeContext);
            context.Add("fetchLiveState", true);
            return context;
        }

        public static void FilterApiFromScript(string data = "")
        {
            /*LoaderOptions options = new LoaderOptions()
            {
                IsResourceLoadingEnabled = true
            };
            var config = Configuration.Default.WithDefaultLoader(options).WithJs().WithEventLoop();
            var context = BrowsingContext.New(config);
            var doc = context.OpenAsync(req => req.Content(data)).Result;*/
            
            // Trying to get the API urls & data from the "desktop_polymer.js" script.
            //NOTE(ddp): The 'desktop_polymer.js' script contains some useful information as the api path/url's and probably more.
            //EndpointMap: classname/object ---> endpoint/command name : class/function name ---> prototype var ---> function GetApiPaths() ---> object/array api path.
            //EndpointMap is a list of endpoints/commands that are set to a class/function/object, those contains a property named 'prototype' which contains functions. As getApiPaths(), getExtensions(), buildRequest(),
            //The getApiPaths() function returns a array with the api path/url's, the buildRequest() well... builds the request but need to be reversed further don't know how it works yet.
            //Some have more functions that are specific for the api request, need to reverse more of the script. For now it will only try to extract the api path/url.

            // For testing only!
            /*if (jScript.IsNullEmpty())
                return;
            
            MatchCollection collection = Regex.Matches(jScript, @"\w*.EndpointMap:(?!{)([^,]*)");
            List<KeyValuePair<string, string>> endpointMaps = new List<KeyValuePair<string, string>>();
            foreach (Match endpointMatch in collection)
                endpointMaps.Add(SplitEndpointClass(endpointMatch.Value));
            
            MatchCollection collectionEp = Regex.Matches(jScript, @"\w*.EndpointMap:(?={)([^}]*)}");
            Dictionary<string, List<KeyValuePair<string, string>>> endpointMapsDict = new Dictionary<string, List<KeyValuePair<string, string>>>();
            foreach (Match match in collectionEp)
            {
                string endpointMapName = Regex.Match(match.Value, @"\w*.EndpointMap").Value;
                Match m = Regex.Match(match.Value, @"(?<=\{)([^}]*)");
                var splitted = m.Value.Split(',');
                List<KeyValuePair<string, string>> valPairs = new List<KeyValuePair<string, string>>();
                foreach (string res in splitted)
                    valPairs.Add(SplitEndpointClass(res));
                endpointMapsDict.Add(endpointMapName, valPairs);
            }*/
        }
        private static KeyValuePair<string, string> SplitEndpointClass(string value)
        {
            string name = Regex.Match(value, @"\w*(?=:)").Value;
            string className = value.Split(':')[1];
            return new KeyValuePair<string, string>(name, className);
        }
    }
    public struct EndpointMaps
    {
        public List<Prototype> CommandEndpointMap { get; set; }
        public List<Prototype> SignalEndpointMap { get; set; }
        public List<Prototype> ContinuationEndpointMap { get; set; }
        public WatchEndpointMap WatchEndpointMap { get; set; }
        public ReelWatchEndpointMap ReelWatchEndpointMap { get; set; }
    }

    public struct Prototype
    {
        public string ApiPath { get; set; }
    }

    public struct WatchEndpointMap
    {
        public Prototype Player { get; set; }
        public Prototype WatchNext { get; set; }
    }

    public struct ReelWatchEndpointMap
    {
        public Prototype Player { get; set; }
        public Prototype ReelItemWatch { get; set; }
        public Prototype ReelWatchSequence { get; set; }
    }
    public enum ApiRequestType
    {
        Account,
        Search,
        Guide,
        Playlist,
        Home,
        HomeBrowse,
        Channel,
        Video
    }
    public struct ApiRequest
    {
        public JObject Payload { get; set; }
        public string ApiUrl { get; set; }
        public bool RequireAuthentication { get; set; }
        public HttpMethod Method { get; set; }
        public ResponseContentType ContentType { get; set; }
    }
    public enum ResponseContentType
    {
        JSON,
        HTML,
        NULL
    }
    public class RequestPayload
    {
        [JsonProperty("context")]
        public ContextPayload Context { get; set; }
        
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }

        [JsonProperty("continuation")]
        public string Continuation { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
    }
    public class ContextPayload
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("client")]
        public ClientPayload Client { get; set; }
    }
    public class ClientPayload
    {
        [JsonProperty("clientName")]
        public string ClientName { get; set; }

        [JsonProperty("clientVersion")]
        public string ClientVersion { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("hl")]
        public string Hl { get; set; }

        [JsonProperty("gl")]
        public string Gl { get; set; }
    }
}