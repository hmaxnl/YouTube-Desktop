using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class RichSectionRenderer : ITrackingParams
    {
        [JsonProperty("content")]
        public RichSectionContent RichSectionContent { get; set; }
        public string TrackingParams { get; set; }
    }
}