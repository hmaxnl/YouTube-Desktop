using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class Badge : ITrackingParams
    {
        [JsonProperty("iconType")]
        public string IconType { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("tooltip")]
        public string Tooltip { get; set; }

        public string TrackingParams { get; set; }
    }
}