using Newtonsoft.Json;
using YouTubeScrap.Core;

namespace YouTubeScrap.Data.Extend
{
    public class Accessibility
    {
        [JsonProperty("AccessibilityData")]
        public AccessibilityData AccessibilityData { get; set; } = new AccessibilityData();

        [JsonProperty("label")]
        public string Label { get; set; } = string.Empty;
        public string GetText => Label.IsNullEmpty() ? AccessibilityData.Label : Label;
    }

    public class AccessibilityData
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }
}