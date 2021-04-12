using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public class TopbarLogo : ITrackingParams
    {
        [JsonProperty("iconImage")]
        public string Icon { get; set; }
        [JsonProperty("tooltip")]
        public string TooltipText { get; set; }
        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }

        public string TrackingParams { get; set; }
    }
}