using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Video
{
    public class VideoDetails
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("lengthSeconds")]
        public long LengthSeconds { get; set; }
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
        [JsonProperty("isOwnerViewing")]
        public bool IsOwnerViewing { get; set; }
        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }
        [JsonProperty("isCrawlable")]
        public bool IsCrawlable { get; set; }
        [JsonProperty("thumbnail")]
        public List<UrlImage> Thumbnails { get; set; }
        [JsonProperty("averageRating")]
        public double AverageRating { get; set; }
        [JsonProperty("allowRating")]
        public bool AllowRating { get; set; }
        [JsonProperty("viewCount")]
        public string ViewCount { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }
        [JsonProperty("isUnpluggedCorpus")]
        public bool IsUnpluggedCorpus { get; set; }
        [JsonProperty("isLiveContent")]
        public bool IsLiveContent { get; set; }
    }
}