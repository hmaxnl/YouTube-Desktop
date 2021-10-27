using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class RichGridRenderer : ITrackingParams
    {
        // Contents
        public string TrackingParams { get; set; }
        // Header
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
        [JsonProperty("reflowOptions")]
        public ReflowOptions ReflowOptions { get; set; }
    }
}