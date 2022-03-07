using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data.Extend.Endpoints;
using HttpMethod = System.Net.Http.HttpMethod;

namespace YouTubeScrap.Handlers
{
    public static class YoutubeApiManager
    {
        public static ApiRequest PrepareApiRequest(ApiRequestType requestType, PrepData pData)
        {
            ApiRequest apiRequest = new ApiRequest();
            switch (requestType)
            {
                case ApiRequestType.Account:
                    apiRequest.Payload = DefaultRequired(pData.User);
                    apiRequest.ApiUrl = $"/youtubei/v1/account/account_menu?key={pData.User.ClientData.ApiKey}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = true;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Search:
                    if (pData.Query.IsNullEmpty())
                        break;
                    apiRequest.Payload = DefaultRequired(pData.User);
                    apiRequest.Payload.Add("query", pData.Query);
                    apiRequest.Payload.Add("continuation", pData.Continuation);
                    apiRequest.ApiUrl = $"/youtubei/v1/search?key={pData.User.ClientData.ApiKey}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Guide:
                    apiRequest.Payload = DefaultRequired(pData.User);
                    apiRequest.ApiUrl = $"/youtubei/v1/guide?key={pData.User.ClientData.ApiKey}";
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
                    apiRequest.Payload = DefaultRequired(pData.User);
                    apiRequest.Payload.Add("continuation", pData.Continuation);
                    if (!pData.Id.IsNullEmpty())
                        apiRequest.Payload.Add("browseId", pData.Id);
                    apiRequest.ApiUrl = $"/youtubei/v1/browse?key={pData.User.ClientData.ApiKey}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Channel:
                    break;
                case ApiRequestType.Video:
                    apiRequest.ApiUrl = $"/watch?v={pData.Id}";// HTML
                    apiRequest.Method = HttpMethod.Get;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.HTML;
                    break;
                case ApiRequestType.Continuation:
                    break;
                case ApiRequestType.Endpoint:
                    apiRequest.Payload = DefaultRequired(pData.User);
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    switch (pData.Endpoint)
                    {
                        case ContinuationEndpoint ce:
                            apiRequest.ApiUrl = $"{ce.CommandMetadata.ApiUrl}?key={pData.User.ClientData.ApiKey}";
                            if (ce.CommandMetadata.SendPost)
                                apiRequest.Method = HttpMethod.Post;
                            if (ce.Command.TryGetValue("token", out JToken contiToken))
                                apiRequest.Payload.Add("continuation", contiToken.ToString());
                            break;
                    }
                    break;
            }
            return apiRequest;
        }

        public static PrepData BuildPrep(YoutubeUser user, string query = null, string continuation = null, string id = null, object endpoint = null)
        {
            return new PrepData() { User = user, Query = query, Continuation = continuation, Id = id, Endpoint = endpoint};
        }
        public static void FilterApiFromScript(string data = "")
        {
            //TODO: If we want all the api paths and payloads, we gonna need something like V8.Net or JavaScript.Net to call the required js functions.
            // Trying to get the API urls & data from the "desktop_polymer.js" script.
            //NOTE(ddp): The 'desktop_polymer.js' script contains some useful information as the api path/url's and probably more.
            //EndpointMap: classname/object ---> endpoint/command name : class/function name ---> prototype var ---> function GetApiPaths() ---> object/array api path.
            //EndpointMap is a list of endpoints/commands that are set to a class/function/object, those contains a property named 'prototype' which contains functions. As getApiPaths(), getExtensions(), buildRequest(),
            //The getApiPaths() function returns a array with the api path/url's, the buildRequest() well... builds the request but need to be reversed further don't know how it works yet.
            //Some have more functions that are specific for the api request, need to reverse more of the script. For now it will only try to extract the api path/url.

            //string[] endpointMaps = new[] { "commandEndpointMap:", "signalEndpointMap:", "continuationEndpointMap:", "watchEndpointMap:", "reelWatchEndpointMap:" };
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
        private static JObject DefaultRequired(YoutubeUser ytUser)
        {
            JObject innertubeContext = ytUser.ClientData.InnertubeContext;
            if (innertubeContext == null) return null;
            innertubeContext["client"]["visitorData"] = ytUser.ClientData.SboxSettings.VISITORDATA;
            JObject context = new JObject();
            context.Add("context", innertubeContext);
            return context;
        }
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
        Video,
        Continuation,
        Consent,
        Endpoint
    }
    public struct ApiRequest
    {
        public JObject Payload { get; set; }
        public string ApiUrl { get; set; }
        public bool RequireAuthentication { get; set; }
        public HttpMethod Method { get; set; }
        public ResponseContentType ContentType { get; set; }
    }

    public struct PrepData
    {
        public YoutubeUser User;
        public string Query;
        public string Continuation;
        public string Id;
        public object Endpoint;
    }
    public enum ResponseContentType
    {
        NULL,
        JSON,
        HTML,
        JS
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