using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class Interstitial : ITrackingParams
    {
        [JsonProperty("interstitialLogoAside")]
        public TextElement InterstitialLogoAside { get; set; }
        [JsonProperty("languagePickerButton")]
        public ButtonRenderer LanguagePickerButtonRenderer { get; set; }
        [JsonProperty("interstitialTitle")]
        public TextElement InterstitialTitle { get; set; }
        [JsonProperty("interstitialMessage")]
        public TextElement InterstitialMessage { get; set; }
        [JsonProperty("customizeButton")]
        public ButtonRenderer CustomizeButtonRenderer { get; set; }
        [JsonProperty("agreeButton")]
        public ButtonRenderer AgreeButtonRenderer { get; set; }
        [JsonProperty("privacyLink")]
        public TextElement PrivacyLink { get; set; }
        [JsonProperty("termsLink")]
        public TextElement TermsLink { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("signInButton")]
        public ButtonRenderer SignInButtonRenderer { get; set; }
        [JsonProperty("v21Message")]
        public V21Message V21Message { get; set; }
        [JsonProperty("languageList")]
        public LanguageList LanguageList { get; set; }
        [JsonProperty("readMoreButton")]
        public ButtonRenderer ReadMoreButtonRenderer { get; set; }
        [JsonProperty("loadingMessage")]
        public TextElement LoadingMessage { get; set; }
        [JsonProperty("errorMessage")]
        public TextElement ErrorMessage { get; set; }
    }
}