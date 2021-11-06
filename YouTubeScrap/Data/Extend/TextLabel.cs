using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class TextLabel
    {
        [JsonProperty("accessibility.accessibilityData.label")]
        public string Label { get; set; }
        [JsonProperty("simpleText")]
        public string SimpleText { get; set; }
        [JsonProperty("runs")]
        public List<TextRun> Runs { get; set; }
    }
}