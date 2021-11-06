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
        [JsonProperty("descriptionSnippet.runs")]
        public List<TextRun> DescriptionSnippet { get; set; }
        [JsonProperty("longBylineText.runs")]
        public List<TextRun> LongByLineText { get; set; }
        [JsonProperty("publishedTimeText.simpleText")]
        public string PublishedTimeText { get; set; }
        [JsonProperty("lengthText")]
        public TextLabel LengthText { get; set; }
        [JsonProperty("viewCountText")]
        public ViewCountText ViewCountText { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("ownerBadges")]
        public List<Badge> OwnerBadges { get; set; }
        [JsonProperty("ownerText.runs")]
        public List<TextRun> OwnerText { get; set; }
        [JsonProperty("shortByLineText.runs")]
        public List<TextRun> ShortByLineText { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("showActionMenu")]
        public bool ShowActionMenu { get; set; }
        [JsonProperty("shortViewCountText")]
        public ViewCountText ShortViewCountText { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("channelThumbnailSupportedRenderers")]
        public ChannelThumbnailSupportedRenderers ChannelThumbnail { get; set; }
        [JsonProperty("thumbnailOverlays")]
        public List<ThumbnailOverlay> ThumbnailOverlays { get; set; }
        [JsonProperty("accessibility.accessibilityData.label")]
        public string AccessibilityLabel { get; set; }
        // richThumbnail
    }
}