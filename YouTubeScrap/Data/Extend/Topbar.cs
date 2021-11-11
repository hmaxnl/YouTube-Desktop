using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class Topbar : ITrackingParams
    {
        [JsonProperty("logo")]
        public TopbarLogo Logo { get; set; }
        [JsonProperty("searchbox")]
        public Searchbox Searchbox { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("interstitial")]
        public Interstitial Interstitial { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("topbarButtons")]
        public List<TopbarButton> TopbarButtons { get; set; }
        [JsonProperty("hotkeyDialog")]
        public HotkeyDialog HotkeyDialog { get; set; }
        [JsonProperty("backButton")]
        public Button BackButton { get; set; }
        [JsonProperty("forwardButton")]
        public Button ForwardButton { get; set; }
        [JsonProperty("a11ySkipNavigationButton")]
        public Button A11ySkipNavigationButton { get; set; }
        [JsonProperty("voiceSearchButton")]
        public Button VoiceSearchButton { get; set; }
    }
}