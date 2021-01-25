using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

using Newtonsoft.Json;

using YouTubeScrap.Util;

namespace YouTubeScrap.Models.Video.PlayerResponse
{
    public class Microformat
    {
        [JsonProperty("thumbnail")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("title")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string Title { get; set; }
        [JsonProperty("description")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string Desciption { get; set; }
        [JsonProperty("lengthSeconds")]
        public long LengthInSeconds { get; set; }
        [JsonProperty("ownerProfileUrl")]
        public string OwnerProfileUrl { get; set; }
        [JsonProperty("externalChannelId")]
        public string ExternalChannelId { get; set; }
        [JsonProperty("isFamilySafe")]
        public bool IsFamilySafe { get; set; }
        [JsonProperty("availableCountries")]
        public List<string> AvailableCountries { get; set; }
        [JsonProperty("isUnlisted")]
        public bool IsUnlisted { get; set; }
        [JsonProperty("hasYpcMetadata")]
        public bool HasYpcMetadata { get; set; }
        [JsonProperty("viewCount")]
        public long ViewCount { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("publishDate")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public DateTime PublishDate { get; set; }
        [JsonProperty("ownerChannelName")]
        public string OwnerChannelName { get; set; }
        [JsonProperty("liveBroadcastDetails")]
        public BroadcastDetails LiveBroadcastDetails { get; set; }
        [JsonProperty("uploadDate")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public DateTime UploadDate { get; set; }
    }
    public struct BroadcastDetails
    {
        [JsonProperty("isLiveNow")]
        public bool IsLiveNow { get; set; }
        [JsonProperty("startTimestamp")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public DateTime StartTimeStamp { get; set; }
        [JsonProperty("endTimestamp")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public DateTime EndTimeStamp { get; set; }
    }
}