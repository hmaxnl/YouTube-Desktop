using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ThumbnailOverlayToggleButtonRenderer : ITrackingParams
    {
        [JsonProperty("isToggled")]
        public bool IsToggled { get; set; }
        [JsonProperty("untoggledIcon")]
        public string UntoggledIcon { get; set; }
        [JsonProperty("toggledIcon")]
        public string ToggledIcon { get; set; }
        [JsonProperty("untoggledTooltip")]
        public string UntoggledTooltip { get; set; }
        [JsonProperty("toggledTooltip")]
        public string ToggledTooltip { get; set; }
        [JsonProperty("untoggledAccessibility.accessibilityData.label")]
        public string UntoggledAccessibilityLabel { get; set; }
        [JsonProperty("toggledAccessibility.accessibilityData.label")]
        public string ToggledAccessibilityLabel { get; set; }
        [JsonProperty("untoggledServiceEndpoint")]
        public ToggleServiceEndpoint UntoggledServiceEndpoint { get; set; }
        [JsonProperty("toggledServiceEndpoint")]
        public ToggleServiceEndpoint ToggledServiceEndpoint { get; set; }
        [JsonProperty("untoggledLabel")]
        public string UntoggledLabel { get; set; }
        [JsonProperty("toggledLabel")]
        public string ToggledLabel { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }
}