using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
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
        [JsonProperty("untoggledAccessibility")]
        public Accessibility UntoggledAccessibility { get; set; }
        [JsonProperty("toggledAccessibility")]
        public Accessibility ToggledAccessibility { get; set; }
        [JsonProperty("untoggledServiceEndpoint")]
        public ToggledServiceEndpoint UntoggledServiceEndpoint { get; set; }
        [JsonProperty("toggledServiceEndpoint")]
        public ToggledServiceEndpoint ToggledServiceEndpoint { get; set; }
        [JsonProperty("untoggledLabel")]
        public string UntoggledLabel { get; set; }
        [JsonProperty("toggledLabel")]
        public string ToggledLabel { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }
}