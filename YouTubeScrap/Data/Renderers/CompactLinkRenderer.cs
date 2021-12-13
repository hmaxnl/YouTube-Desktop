using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class CompactLinkRenderer : ITrackingParams
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("title")]
        public TextElement Title { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        public string TrackingParams { get; set; }
    }
}