using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class Thumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("width")]
        public long Width { get; set; }
        [JsonProperty("height")]
        public long Height { get; set; }
    }
}