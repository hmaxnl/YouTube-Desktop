using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class Card
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("params")]
        public string Params { get; set; }
        [JsonProperty("query")]
        public string Query { get; set; }
        //[JsonProperty("webCommandMetadata")]
        //public CommandMetadata WebCommandMetadata { get; set; }
        public string TrackingParams { get; set; }
    }
}