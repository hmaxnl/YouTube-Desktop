using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class UrlEndpoint
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("target")]
        public string Target { get; set; }
    }
}