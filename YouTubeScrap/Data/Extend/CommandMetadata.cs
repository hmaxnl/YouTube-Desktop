using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class CommandMetadata
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("apiUrl")]
        public string ApiUrl { get; set; }
        [JsonProperty("webPageType")]
        public string WebPageType { get; set; }
        [JsonProperty("rootVe")]
        public int RootVe { get; set; }
        [JsonProperty("ignoreNavigation")]
        public bool IgnoreNavigation { get; set; }
        [JsonProperty("sendPost")]
        public bool SendPost { get; set; }
    }
}