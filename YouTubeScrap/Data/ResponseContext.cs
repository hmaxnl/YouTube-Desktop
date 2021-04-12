using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data
{
    public class ResponseContext
    {
        [JsonProperty("serviceTrackingParams")]
        public List<TrackingParam> ServiceTrackingParams { get; set; }
        [JsonProperty("mainAppWebResponseContext")]
        public MainAppWebResponseContext MainAppWebResponseContext { get; set; }
        [JsonProperty("webResponseContextExtensionData")]
        public WebResponseContextED WebResponseContextED { get; set; }
    }
    public struct MainAppWebResponseContext
    {
        [JsonProperty("datasyncId")]
        public string DatasyncId { get; set; }
        [JsonProperty("loggedOut")]
        public bool LoggedOut { get; set; }
    }
    public struct WebResponseContextED
    {
        [JsonProperty("hasDecorated")]
        public bool HasDecorated { get; set; }
    }
}