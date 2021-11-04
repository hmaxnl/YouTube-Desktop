using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class TextLabel
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("simpleText")]
        public string SimpleText { get; set; }
    }
}