using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class MenuButtonRenderer : ITrackingParams
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("menuRequest")]
        public MenuRequest MenuRequest { get; set; }
        [JsonProperty("menuRenderer")]
        public MenuRenderer MenuRenderer { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
        [JsonProperty("tooltip")]
        public string Tooltip { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
    }
}