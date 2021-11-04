using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    public class ThumbnailOverlay : ITrackingParams
    {
        [JsonProperty("text")] //TODO: Need to fix different text types in JSON.
        [JsonConverter(typeof(JsonDeserializeFilter))]
        public TextLabel Text { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        
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
        [JsonProperty("untoggledServiceEndpoint")]
        public ToggleServiceEndpoint UntoggledServiceEndpoint { get; set; }
        [JsonProperty("toggledServiceEndpoint")]
        public ToggleServiceEndpoint ToggledServiceEndpoint { get; set; }
        [JsonProperty("untoggledLabel")]
        public string UntoggledLabel { get; set; }
        [JsonProperty("toggledLabel")]
        public string ToggledLabel { get; set; }
        public string TrackingParams { get; set; }
    }
}