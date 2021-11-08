using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class TimeStamp
    {
        [JsonProperty("seconds")]
        public string Seconds { get; set; }
        [JsonProperty("nanos")]
        public long Nanos { get; set; }
    }
}