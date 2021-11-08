using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class RichItemRenderer : ITrackingParams
    {
        [JsonProperty("content")]
        public RichItemContent RichItemContent { get; set; }
        public string TrackingParams { get; set; }
    }
}