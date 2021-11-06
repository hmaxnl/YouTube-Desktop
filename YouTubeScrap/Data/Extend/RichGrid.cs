using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class RichGrid
    {
        [JsonProperty("contents")]
        public List<ContentRender> Contents { get; set; }
        public string TrackingParams { get; set; }
        // Header
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
        [JsonProperty("masthead")]
        public Masthead Masthead { get; set; }
        [JsonProperty("reflowOptions")]
        public ReflowOptions ReflowOptions { get; set; }
    }
}