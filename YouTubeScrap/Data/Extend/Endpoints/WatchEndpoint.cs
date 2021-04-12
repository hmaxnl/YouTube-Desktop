using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class WatchEndpoint
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("startTimeSeconds")]
        public long StartTimeSeconds { get; set; }
    }
}