using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Renderers;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Badge
    {
        [JsonProperty("metadataBadgeRenderer.iconType")]
        public string IconType { get; set; }
        [JsonProperty("metadataBadgeRenderer.icon")]
        public string Icon { get; set; }
        [JsonProperty("metadataBadgeRenderer.style")]
        public string Style { get; set; }
        [JsonProperty("metadataBadgeRenderer.accessibilityData.label")]
        public string AccessibilityLabel { get; set; }
        [JsonProperty("metadataBadgeRenderer.label")]
        public string Label { get; set; }
        [JsonProperty("metadataBadgeRenderer.tooltip")]
        public string Tooltip { get; set; }
        [JsonProperty("metadataBadgeRenderer.trackingParams")]
        public string TrackingParams { get; set; }
    }
}