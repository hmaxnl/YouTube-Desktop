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
        public TextLabel Personalization { get; set; }
        [JsonProperty("customizationOption")]
        public TextLabel CustomizationOption { get; set; }
    }
}