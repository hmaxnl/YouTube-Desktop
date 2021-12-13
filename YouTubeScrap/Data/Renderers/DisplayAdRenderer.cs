using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class DisplayAdRenderer : ITrackingParams
    {
        public string TrackingParams { get; set; }
        [JsonProperty("layout")]
        public string Layout { get; set; }
        [JsonProperty("titleText")]
        public TextElement TitleText { get; set; }
        [JsonProperty("image")]
        public List<Thumbnail> Image { get; set; }
        [JsonProperty("bodyText")]
        public TextElement BodyText { get; set; }
        [JsonProperty("secondaryText")]
        public TextElement SecondaryText { get; set; }
        [JsonProperty("badge")]
        public Badge Badge { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("ctaButton")]
        public Button CtaButton { get; set; }
        [JsonProperty("impressionEndpoints")]
        public List<ImpressionEndpoint> ImpressionEndpoints { get; set; }
        [JsonProperty("clickCommand")]
        public Command ClickCommand { get; set; }
        [JsonProperty("mediaHoverOverlay")]
        public Button MediaHoverOverlay { get; set; }
        [JsonProperty("mediaBadge")]
        public Badge MediaBadge { get; set; }
    }
}