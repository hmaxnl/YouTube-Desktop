using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayLoadingPreviewRenderer
    {
        [JsonProperty("text")]
        public TextElement Text { get; set; }
    }
}