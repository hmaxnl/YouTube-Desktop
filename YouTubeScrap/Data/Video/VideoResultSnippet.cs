using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Channel;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Video
{
    public class VideoResultSnippet : IContent
    {
        [JsonProperty("title")]
        public Title Title { get; set; }
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("thumbnails")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("thumbnailSet")]
        public ThumbnailSet ThumbnailSet { get; set; }
        [JsonProperty("richThumbnails")]
        public List<Thumbnail> RichThumbnails { get; set; }
        [JsonProperty("descriptionSnippet")]
        public string DescriptionSnippet { get; set; }
        [JsonProperty("publishedTimeText")]
        public string PublishedTimeText { get; set; }
        [JsonProperty("videoLength")]
        public LengthCounts VideoLength { get; set; }
        [JsonProperty("videoViewCount")]
        public ViewCounts VideoViewCount { get; set; }
        [JsonProperty("likes")]
        public LikeCounts Likes { get; set; }
        [JsonProperty("dislikes")]
        public LikeCounts Dislikes { get; set; }
        [JsonProperty("ownerBadges")]
        public List<Badge> OwnerBadges { get; set; }
        [JsonProperty("badges")]
        public List<Badge> Badges { get; set; }
        [JsonProperty("channelName")]
        public string ChannelName { get; set; }
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
        [JsonProperty("channelVideoResult")]
        public ChannelVideoResult Channel { get; set; }
        [JsonProperty("showActionMenu")]
        public bool ShowActionMenu { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("percentDurationWatched")]
        public int PercentDurationWatched { get; set; }


        public ContentIdentifier Kind { get; set; }
        public Type Identifier { get => GetType(); }
        public string TrackingParams { get; set; }
    }
}