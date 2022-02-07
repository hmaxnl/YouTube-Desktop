using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class RichVideoContent : ITrackingParams
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("thumbnails")]
        public List<UrlImage> Thumbnails { get; set; }
        [JsonProperty("title")]
        public TextElement Title { get; set; }
        [JsonProperty("descriptionSnippet")]
        public TextElement DescriptionSnippet { get; set; }
        [JsonProperty("longBylineText")]
        public TextElement LongByLineText { get; set; }
        [JsonProperty("publishedTimeText")]
        public TextElement PublishedTimeText { get; set; }
        [JsonProperty("lengthText")]
        public TextElement LengthText { get; set; }
        [JsonProperty("viewCountText")]
        public TextElement ViewCountText { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("ownerBadges")]
        public List<Badge> OwnerBadges { get; set; }
        [JsonProperty("ownerText")]
        public TextElement OwnerText { get; set; }
        [JsonProperty("upcomingEventData")]
        public UpcomingEventData UpcomingEvent { get; set; }
        [JsonProperty("shortByLineText")]
        public TextElement ShortByLineText { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("showActionMenu")]
        public bool ShowActionMenu { get; set; }
        [JsonProperty("shortViewCountText")]
        public TextElement ShortViewCountText { get; set; }
        [JsonProperty("isWatched")]
        public bool IsWatched { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("channelThumbnailSupportedRenderers")]
        public ChannelThumbnailSupportedRenderers ChannelThumbnail { get; set; }
        [JsonProperty("thumbnailOverlays")]
        public List<ThumbnailOverlay> ThumbnailOverlays { get; set; }
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
        [JsonProperty("richThumbnail")]
        public RichThumbnail RichThumbnail { get; set; }

        public OverlayVariables OverlayData => _overlayData ??= new OverlayVariables(ThumbnailOverlays);
        private OverlayVariables _overlayData;
    }
}