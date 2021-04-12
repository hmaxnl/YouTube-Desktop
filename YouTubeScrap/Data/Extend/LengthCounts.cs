using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class LengthCounts
    {
        [JsonProperty("lengthCount")]
        public string LengthCount { get; set; }
        [JsonProperty("shortLengthCount")]
        public string ShortLengthCount { get; set; }
    }
}