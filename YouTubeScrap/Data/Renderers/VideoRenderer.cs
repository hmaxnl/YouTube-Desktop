using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class VideoRenderer : ITrackingParams
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("title")]
        public TextLabel Title { get; set; }
        [JsonProperty("descriptionSnippet")]
        public TextLabel DescriptionSnippet { get; set; }
        [JsonProperty("longBylineText")]
        public TextLabel LongByLineText { get; set; }
        [JsonProperty("publishedTimeText.simpleText")]
        public string PublishedTimeText { get; set; }
        [JsonProperty("lengthText")]
        public TextLabel LengthText { get; set; }
        [JsonProperty("viewCountText")]
        public TextLabel ViewCountText { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("ownerBadges")]
        public List<Badge> OwnerBadges { get; set; }
        [JsonProperty("ownerText")]
        public TextLabel OwnerText { get; set; }
        [JsonProperty("shortByLineText")]
        public TextLabel ShortByLineText { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("showActionMenu")]
        public bool ShowActionMenu { get; set; }
        [JsonProperty("shortViewCountText")]
        public TextLabel ShortViewCountText { get; set; }
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
        [JsonProperty("richThumbnail.movingThumbnailRenderer")]
        public RichThumbnail RichThumbnail { get; set; }
    }
}