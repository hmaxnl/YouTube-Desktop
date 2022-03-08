using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class Topbar : ITrackingParams
    {
        [JsonProperty("logo")]
        public TopbarLogo Logo { get; set; } = new TopbarLogo();
        [JsonProperty("searchbox")]
        public Searchbox Searchbox { get; set; } = new Searchbox();
        public string TrackingParams { get; set; }
        [JsonProperty("interstitial")]
        public Interstitial Interstitial { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; } = String.Empty;
        [JsonProperty("topbarButtons")]
        public List<TopbarButton> TopbarButtons { get; set; }
        [JsonProperty("hotkeyDialog")]
        public HotkeyDialog HotkeyDialog { get; set; }
        [JsonProperty("backButton")]
        public ButtonRenderer BackButtonRenderer { get; set; }
        [JsonProperty("forwardButton")]
        public ButtonRenderer ForwardButtonRenderer { get; set; }
        [JsonProperty("a11ySkipNavigationButton")]
        public ButtonRenderer A11YSkipNavigationButtonRenderer { get; set; }
        [JsonProperty("voiceSearchButton")]
        public ButtonRenderer VoiceSearchButtonRenderer { get; set; }
    }
}