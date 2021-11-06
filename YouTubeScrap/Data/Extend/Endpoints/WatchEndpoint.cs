using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class WatchEndpoint
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("startTimeSeconds")]
        public long StartTimeSeconds { get; set; }
        [JsonProperty("watchEndpointSupportedOnesieConfig.html5PlaybackOnesieConfig.commonConfig.url")]
        public string WatchEndpointSupportedOnesieConfig { get; set; }
    }
}