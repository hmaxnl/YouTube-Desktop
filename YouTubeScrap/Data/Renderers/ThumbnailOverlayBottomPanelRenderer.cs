using Newtonsoft.Json;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayBottomPanelRenderer
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}