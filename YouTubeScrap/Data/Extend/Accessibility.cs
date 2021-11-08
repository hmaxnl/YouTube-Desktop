using Newtonsoft.Json;
using YouTubeScrap.Core;

namespace YouTubeScrap.Data.Extend
{
    public class Accessibility
    {
        [JsonProperty("AccessibilityData")]
        public AccessibilityData AccessibilityData { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        public string GetText => Label.IsNullEmpty() ? AccessibilityData.Label : Label;
    }

    public class AccessibilityData
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }
}