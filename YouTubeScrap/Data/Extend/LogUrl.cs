using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class LogUrl
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
    }
}