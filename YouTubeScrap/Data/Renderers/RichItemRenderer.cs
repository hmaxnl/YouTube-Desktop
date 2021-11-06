using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class RichItemRenderer : IRichRenderer
    {
        public RichItemContent RichItemContent { get; set; }
        public string TrackingParams { get; set; }
    }
}