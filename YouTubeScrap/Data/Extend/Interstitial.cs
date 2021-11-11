using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class Interstitial : ITrackingParams
    {
        [JsonProperty("interstitialLogoAside")]
        public TextLabel InterstitialLogoAside { get; set; }
        [JsonProperty("languagePickerButton")]
        public Button LanguagePickerButton { get; set; }
        [JsonProperty("interstitialTitle")]
        public TextLabel InterstitialTitle { get; set; }
        [JsonProperty("interstitialMessage")]
        public TextLabel InterstitialMessage { get; set; }
        [JsonProperty("customizeButton")]
        public Button CustomizeButton { get; set; }
        [JsonProperty("agreeButton")]
        public Button AgreeButton { get; set; }
        [JsonProperty("privacyLink")]
        public TextLabel PrivacyLink { get; set; }
        [JsonProperty("termsLink")]
        public TextLabel TermsLink { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("signInButton")]
        public Button SignInButton { get; set; }
        [JsonProperty("v21Message")]
        public V21Message V21Message { get; set; }
        [JsonProperty("languageList")]
        public LanguageList LanguageList { get; set; }
        [JsonProperty("readMoreButton")]
        public Button ReadMoreBUtton { get; set; }
    }
}