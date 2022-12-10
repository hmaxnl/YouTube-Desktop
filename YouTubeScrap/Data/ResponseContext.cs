using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data
{
    public class ResponseContext
    {
        [JsonProperty("serviceTrackingParams")]
        public List<TrackingParam> ServiceTrackingParams { get; set; }
        [JsonProperty("maxAgeSeconds")]
        public int MaxAgeSeconds { get; set; }
        [JsonProperty("mainAppWebResponseContext")]
        public MainAppWebResponseContext MainAppWebResponseContext { get; set; }
        [JsonProperty("webResponseContextExtensionData")]
        public WebResponseContextEd WebResponseContextEd { get; set; }
    }
    public struct MainAppWebResponseContext
    {
        [JsonProperty("datasyncId")]
        public string DatasyncId { get; set; }
        [JsonProperty("loggedOut")]
        public bool LoggedOut { get; set; }
    }
    public struct WebResponseContextEd
    {
        [JsonProperty("hasDecorated")]
        public bool HasDecorated { get; set; }
    }
}