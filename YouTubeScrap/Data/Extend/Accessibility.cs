using System;
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
        public override string ToString() => !Label.IsNullEmpty() ? Label : AccessibilityData != null ? AccessibilityData.Label : string.Empty;
    }

    public class AccessibilityData
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }
}