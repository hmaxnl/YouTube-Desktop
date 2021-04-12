using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;

namespace YouTubeScrap.Data.Extend
{
    public class PropertyText
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }
        [JsonProperty("bold")]
        public bool Bold { get; set; }
    }
}