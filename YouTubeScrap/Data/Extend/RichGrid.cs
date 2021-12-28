using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    public class RichGrid : ITrackingParams
    {
        [JsonProperty("contents")]
        [JsonConverter(typeof(JsonContentConverter))]
        public List<object> Contents { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("header")]
        public Header Header { get; set; }
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
        [JsonProperty("masthead")]
        public Masthead Masthead { get; set; }
        [JsonProperty("reflowOptions")]
        public ReflowOptions ReflowOptions { get; set; }
    }
}