using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayNowPlayingRenderer
    {
        [JsonProperty("text")]
        public TextLabel Text { get; set; }
    }
}