using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class V21Message
    {
        [JsonProperty("essentialCookieMsg")]
        public CookieMessage EssentialCookieMsg { get; set; }
        [JsonProperty("nonEssentialCookieMsg")]
        public CookieMessage NonEssentialCookieMsg { get; set; }
        [JsonProperty("personalization")]
        public TextElement Personalization { get; set; }
        [JsonProperty("customizationOption")]
        public TextElement CustomizationOption { get; set; }
    }
}