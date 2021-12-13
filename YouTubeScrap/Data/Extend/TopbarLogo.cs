using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public class TopbarLogo : ITrackingParams
    {
        [JsonProperty("iconImage")]
        public string IconImage { get; set; }
        [JsonProperty("tooltipText")]
        public TextElement TooltipText { get; set; }
        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("overrideEntityKey")]
        public string OverrideEntityKey { get; set; }
    }
}