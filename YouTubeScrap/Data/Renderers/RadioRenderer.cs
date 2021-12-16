using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class RadioRenderer : ITrackingParams
    {
        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
        [JsonProperty("title")]
        public TextElement Title { get; set; }
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("videoCountText")]
        public TextElement VideoCountText { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("videos")]
        public List<VideoRenderer> Videos { get; set; }
        [JsonProperty("thumbnailText")]
        public TextElement ThumbnailText { get; set; }
        [JsonProperty("longByLineText")]
        public TextElement LongByLineText { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("thumbnailOverlays")]
        public List<ThumbnailOverlay> ThumbnailOverlays { get; set; }
        [JsonProperty("videoCountShortText")]
        public TextElement VideoCountShortText { get; set; }

        public OverlayVariables OverlayData => _overlayData ??= new OverlayVariables(ThumbnailOverlays);
        private OverlayVariables _overlayData;
    }
}