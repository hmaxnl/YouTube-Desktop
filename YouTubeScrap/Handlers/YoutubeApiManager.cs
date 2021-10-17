using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;

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

        private static void FilterApiFromScript(string jScript)
        {
            // For testing only!
            // Trying to get the API urls & data from the "desktop_polymer.js" script.
            if (jScript.IsNullEmpty())
                return;
            
            MatchCollection collection = Regex.Matches(jScript, @"\w*.EndpointMap:(?!{)([^,]*)");
            foreach (Match endpointMatch in collection)
            {
                string name = Regex.Match(endpointMatch.Value, @"\w*.EndpointMap").Value;
                string value = endpointMatch.Value.Split(':')[1];
            }
            
            MatchCollection collectionEp = Regex.Matches(jScript, @"\w*.EndpointMap:(?!.*,)([^}]*)}");// need be updated!
            Dictionary<string, string> endpointMapsDict = new Dictionary<string, string>();
            foreach (Match match in collectionEp)
            {
                string endpointMapName = Regex.Match(match.Value, @"\w*.EndpointMap").Value;
                Match m = Regex.Match(match.Value, @"(?<=\{)([^}]*)");
                endpointMapsDict.Add(endpointMapName, m.Value);
            }
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