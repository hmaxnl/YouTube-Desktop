using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class WebSearchboxConfig
    {
        [JsonProperty("requestLanguage")]
        public string RequestLanguage { get; set; }
        [JsonProperty("requestDomain")]
        public string RequestDomain { get; set; }
        [JsonProperty("hasOnscreenKeyboard")]
        public bool HasOnscreenKeyboard { get; set; }
        [JsonProperty("focusSearchbox")]
        public bool FocusSearchbox { get; set; }
    }
}