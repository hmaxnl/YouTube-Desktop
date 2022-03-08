using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class GuideSigninPromoRenderer
    {
        [JsonProperty("descriptiveText")]
        public TextElement DescriptiveText { get; set; }
        [JsonProperty("actionText")]
        public TextElement ActionText { get; set; }
        [JsonProperty("signInButton")]
        public ButtonRenderer SignInButtonRenderer { get; set; }
    }
}