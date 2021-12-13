using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayTimeStatusRenderer
    {
        [JsonProperty("text")]
        public TextElement Text { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
    }
}