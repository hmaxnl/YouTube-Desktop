using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class Searchbox : ITrackingParams
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("placeholderText")]
        public TextElement PlaceholderText { get; set; } = new TextElement();
        [JsonProperty("config")]
        public Config Config { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("searchEndpoint")]
        public searchEndpoint SearchEndpoint { get; set; }
        [JsonProperty("clearButton")]
        public ButtonRenderer ClearButtonRenderer { get; set; }
    }
}