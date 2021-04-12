using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Media.Data
{
    public class EndScreen
    {
        [JsonProperty("elements")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<EndScreenElement> Elements { get; set; }
        [JsonProperty("startMs")]
        public long StartMs { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }
    public struct EndScreenElement
    {
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("image")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("icon")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<Thumbnail> Icons { get; set; }
        [JsonProperty("videoDuration")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string VideoDuration { get; set; }
        [JsonProperty("left")]
        public double LeftPos { get; set; }
        [JsonProperty("width")]
        public double Width { get; set; }
        [JsonProperty("top")]
        public double TopPos { get; set; }
        [JsonProperty("aspectRatio")]
        public double AspectRatio { get; set; }
        [JsonProperty("startMs")]
        public long StartMs { get; set; }
        [JsonProperty("endMs")]
        public long EndMs { get; set; }
        [JsonProperty("title")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string Title { get; set; }
        [JsonProperty("metadata")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string MetaData { get; set; }
        [JsonProperty("endpoint")]
        public EndpointMetadata EndPoint { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("isSubscribe")]
        public bool IsSubscribed { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public struct EndpointMetadata
    {
        [JsonProperty("clickTrackingParams")]
        public string ClickTrackingParams { get; set; }
        [JsonProperty("watchEndpoint")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string WatchEndpoint { get; set; }
        [JsonProperty("browseEndpoint")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string BrowseEndpoint { get; set; }
    }
}