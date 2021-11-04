using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    //TODO: Need to further implemented!
    public class Badge : ITrackingParams
    {
        [JsonProperty("iconType")]
        public string IconType { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("tooltip")]
        public string Tooltip { get; set; }

        public string TrackingParams { get; set; }
    }
}