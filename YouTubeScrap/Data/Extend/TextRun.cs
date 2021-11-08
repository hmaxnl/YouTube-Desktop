using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;

namespace YouTubeScrap.Data.Extend
{
    public class TextRun
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("bold")]
        public bool Bold { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
    }
}