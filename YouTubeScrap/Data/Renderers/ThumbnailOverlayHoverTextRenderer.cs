using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayHoverTextRenderer
    {
        [JsonProperty("text")]
        public TextLabel Text { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}