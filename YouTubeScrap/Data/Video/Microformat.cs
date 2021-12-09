using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Video.Data;

namespace YouTubeScrap.Data.Video
{
    public class Microformat
    {
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("embed")]
        public Embed Embed { get; set; }
        [JsonProperty("title")]
        public TextLabel Title { get; set; }
        [JsonProperty("description")]
        public TextLabel Description { get; set; }
        [JsonProperty("lengthSeconds")]
        public long LengthSeconds { get; set; }
        [JsonProperty("ownerProfileUrl")]
        public string OwnerProfileUrl { get; set; }
        [JsonProperty("externalChannelId")]
        public string ExternalChannelId { get; set; }
        [JsonProperty("isFamiliySave")]
        public bool IsFamilySave { get; set; }
        [JsonProperty("availableCountries")]
        public List<string> AvailableCountries { get; set; }
        [JsonProperty("isUnlisted")]
        public bool IsUnlisted { get; set; }
        [JsonProperty("hasYpcMetadata")]
        public bool HasYpcMetadata { get; set; }
        [JsonProperty("viewCount")]
        public string ViewCount { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("publishDate")]
        public string PublishDate { get; set; }
        [JsonProperty("ownerChannelName")]
        public string OwnerChannelName { get; set; }
        [JsonProperty("uploadDate")]
        public string UploadDate { get; set; }
    }
}