using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Masthead : ITrackingParams
    {
        [JsonProperty("bannerPromoRenderer.backgroundImage")]
        public List<UrlImage> BackgroundImage { get; set; }
        [JsonProperty("bannerPromoRenderer.logoImage")]
        public List<UrlImage> LogoImage { get; set; }
        [JsonProperty("bannerPromoRenderer.promoText")]
        public TextElement PromoText { get; set; }
        // Action button
        // impressionEndpoints
        [JsonProperty("bannerPromoRenderer.isVisible")]
        public bool IsVisible { get; set; }
        [JsonProperty("bannerPromoRenderer.trackingParams")]
        public string TrackingParams { get; set; }
        // dismissButton
        [JsonProperty("bannerPromoRenderer.style.styleType")]
        public string Style { get; set; }
        [JsonProperty("bannerPromoRenderer.colorData.basicColorPaletteData")]
        public ColorData ColorData { get; set; }
    }
}