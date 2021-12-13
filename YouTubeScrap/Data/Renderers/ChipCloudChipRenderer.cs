using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ChipCloudChipRenderer : ITrackingParams
    {
        [JsonProperty("style.styleType")]
        public string Style { get; set; }
        [JsonProperty("text")]
        public TextElement Text { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
    }
}