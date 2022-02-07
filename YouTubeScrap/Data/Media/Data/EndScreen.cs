using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Media.Data
{
    public class EndScreen
    {
        [JsonProperty("elements")]
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
        public List<UrlImage> Thumbnails { get; set; }
        [JsonProperty("icon")]
        public List<UrlImage> Icons { get; set; }
        [JsonProperty("videoDuration")]
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
        public string Title { get; set; }
        [JsonProperty("metadata")]
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
        public string WatchEndpoint { get; set; }
        [JsonProperty("browseEndpoint")]
        public string BrowseEndpoint { get; set; }
    }
}