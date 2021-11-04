using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class TabRenderer : ITrackingParams
    {
        [JsonProperty("selected")]
        public bool Selected { get; set; }
        [JsonProperty("content")]
        public RichGrid Content { get; set; }
        [JsonProperty("tabIdentifier")]
        public string TabIdentifier { get; set; }
        public string TrackingParams { get; set; }
    }
}