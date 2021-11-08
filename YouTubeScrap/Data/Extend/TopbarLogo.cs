using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public class TopbarLogo : ITrackingParams
    {
        [JsonProperty("topbarLogoRenderer.iconImage")]
        public string IconImage { get; set; }
        [JsonProperty("topbarLogoRenderer.tooltipText")]
        public TextLabel TooltipText { get; set; }
        [JsonProperty("topbarLogoRenderer.endpoint")]
        public Endpoint Endpoint { get; set; }
        [JsonProperty("topbarLogoRenderer.trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("topbarLogoRenderer.overrideEntityKey")]
        public string OverrideEntityKey { get; set; }
    }
}