using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class VideoRenderer : ITrackingParams
    {
        [JsonProperty("kind")]
        public ContentKind Kind { get; set; }
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("title")]
        public List<TextRun> Title { get; set; }
        [JsonProperty("descriptionSnippet")]
        public List<TextRun> DescriptionSnippet { get; set; }
        [JsonProperty("longByLine")]
        public List<TextRun> LongByLineText { get; set; }
        [JsonProperty("publishedTimeText")]
        public string PublishedTimeText { get; set; }
        [JsonProperty("lengthText")]
        public TextLabel LengthText { get; set; }
        /*[JsonProperty("viewCountText")]
        public string ViewCountText { get; set; }*/
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("ownerBadges")]
        public List<Badge> OwnerBadges { get; set; }
        [JsonProperty("ownerText")]
        public List<TextRun> OwnerText { get; set; }
        [JsonProperty("shortByLineText")]
        public List<TextRun> ShortByLineText { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("showActionMenu")]
        public bool ShowActionMenu { get; set; }
        /*[JsonProperty("shortViewCountText")]
        public TextLabel ShortViewCountText { get; set; }*/
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("channelThumbnailWithLinkRenderer")]//TODO: Need to filter out the JSON! 'channelThumbnailSupportedRenderers'
        public ChannelThumbnailWithLinkRenderer ChannelThumbnail { get; set; }
        [JsonProperty("thumbnailOverlays")]
        public List<ThumbnailOverlay> ThumbnailOverlays { get; set; }
        // richThumbnail
    }
}