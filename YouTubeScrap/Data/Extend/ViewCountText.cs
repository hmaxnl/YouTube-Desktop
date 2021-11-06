using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class ViewCountText
    {
        [JsonProperty("runs")]
        public List<TextRun> Text { get; set; }
        [JsonProperty("accessibility.accessibilityData.label")]
        public string Label { get; set; }
        [JsonProperty("simpleText")]
        public string SimpleText { get; set; }
    }
}