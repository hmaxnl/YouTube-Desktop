using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Video
{
    public class VideoDataSnippet
    {
        [JsonProperty("responseContext")]
        public ResponseContext ResponseContext { get; set; }
        [JsonProperty("playabiblityStatus")]
        public PlayabilityStatus PlayabilityStatus { get; set; }
        [JsonProperty("streamingData")]
        public StreamingData StreamingData { get; set; }
        [JsonProperty("videoDetails")]
        public VideoDetails VideoDetails { get; set; }
        [JsonProperty("microformat")]
        public Microformat Microformat { get; set; }
    }
}