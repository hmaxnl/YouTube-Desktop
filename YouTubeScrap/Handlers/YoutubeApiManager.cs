using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core;

namespace YouTubeScrap.Handlers
{
    public static class YoutubeApiManager
    {
        public static string INNERTUBE_API_KEY { get => "AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8"; }
        public static string CreateJsonRequestPayload(ApiRequest request)
        {
            if (request.Payload == null)
                return null;
            return JsonConvert.SerializeObject(request.Payload, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
        public static ApiRequest PrepareApiRequest(ApiRequestType requestType, string query = null, string continutation = null, string id = null)
        {
            ApiRequest apiRequest = new ApiRequest();
            switch (requestType)
            {
                case ApiRequestType.Account:
                    apiRequest.Payload = DefaultRequired();
                    apiRequest.ApiUrl = $"/youtubei/v1/account/account_menu?key={INNERTUBE_API_KEY}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = true;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Search:
                    if (query.IsNullEmpty())
                        break;
                    apiRequest.Payload = DefaultRequired();
                    apiRequest.Payload.Query = query;
                    apiRequest.Payload.Continuation = continutation;
                    apiRequest.ApiUrl = $"/youtubei/v1/search?key={INNERTUBE_API_KEY}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Guide:
                    apiRequest.Payload = DefaultRequired();
                    apiRequest.ApiUrl = $"/youtubei/v1/guide?key={INNERTUBE_API_KEY}";
                    apiRequest.Method = HttpMethod.Post;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.JSON;
                    break;
                case ApiRequestType.Playlist:
                    break;
                case ApiRequestType.Home:
                    apiRequest.ApiUrl = NetworkHandler.Origin;
                    apiRequest.Method = HttpMethod.Get;
                    apiRequest.RequireAuthentication = false;
                    apiRequest.ContentType = ResponseContentType.HTML;
                    break;
                case ApiRequestType.HomeBrowse:
                    apiRequest.Payload = DefaultRequired();
                    apiRequest.Payload.Continuation = continutation;
                    apiRequest.Payload.BrowseId = "FEwhat_to_watch";
                    apiRequest.ApiUrl = $"/youtubei/v1/browse?key={INNERTUBE_API_KEY}";
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
        private static RequestPayload DefaultRequired()
        {
            ClientPayload clientPayload = new ClientPayload()
            {
                ClientName = "WEB",
                ClientVersion = "2.20210210.08.00",
                Platform = "DESKTOP",
                Hl = "nl",
                Gl = "NL" 
            };
            ContextPayload contextPayload = new ContextPayload()
            { Client = clientPayload };
            RequestPayload payload = new RequestPayload()
            { Context = contextPayload };
            return payload;
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
        Video
    }
    public struct ApiRequest
    {
        public RequestPayload Payload { get; set; }
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