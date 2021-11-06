using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class ThumbnailOverlay : ITrackingParams
    {
        [JsonProperty("thumbnailOverlayTimeStatusRenderer")]
        public ThumbnailOverlayTimeStatusRenderer TimeStatusRenderer { get; set; }
        [JsonProperty("thumbnailOverlayToggleButtonRenderer")]
        public ThumbnailOverlayToggleButtonRenderer ToggleButtonRenderer { get; set; }
        [JsonProperty("thumbnailOverlayNowPlayingRenderer")]
        public ThumbnailOverlayNowPlayingRenderer NowPlayingRenderer { get; set; }
        [JsonProperty("thumbnailOverlayHoverTextRenderer")]
        public ThumbnailOverlayHoverTextRenderer HoverTextRenderer { get; set; }
        [JsonProperty("thumbnailOverlayBottomPanelRenderer")]
        public ThumbnailOverlayBottomPanelRenderer BottomPanelRenderer { get; set; }
        
        public string TrackingParams { get; set; }
    }
}