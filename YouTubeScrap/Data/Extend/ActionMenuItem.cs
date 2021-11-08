using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ActionMenuItem : ITrackingParams
    {
        [JsonProperty("menuServiceItemRenderer.text")]
        public TextLabel Text { get; set; }
        [JsonProperty("menuServiceItemRenderer.icon")]
        public string Icon { get; set; }
        [JsonProperty("menuServiceItemRenderer.iconType")]
        public string IconType { get; set; }
        [JsonProperty("menuServiceItemRenderer.serviceEndpoint")]
        public ServiceEndpoint ServiceEndpoint { get; set; }
        [JsonProperty("menuServiceItemRenderer.trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("menuServiceItemRenderer.hasSeparator")]
        public bool HasSeparator { get; set; }
    }
}