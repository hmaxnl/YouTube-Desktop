using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class DisplayAdRenderer : ITrackingParams
    {
        public string TrackingParams { get; set; }
        [JsonProperty("layout")]
        public string Layout { get; set; }
        [JsonProperty("titleText")]
        public string TitleText { get; set; }
        [JsonProperty("image")]
        public List<Thumbnail> Image { get; set; }
        [JsonProperty("bodyText")]
        public string BodyText { get; set; }
        [JsonProperty("secondaryText")]
        public string SecondaryText { get; set; }
        [JsonProperty("badge")]
        public Badge Badge { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        // ctaButton
        // endpoints
        // click command
        // mediaHoverOverlay
        [JsonProperty("mediaBadge")]
        public Badge MediaBadge { get; set; }
    }
}