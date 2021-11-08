using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class Config
    {
        [JsonProperty("webSearchboxConfig")]
        public WebSearchboxConfig WebSearchboxConfig { get; set; }
    }
}