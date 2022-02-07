using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class CompactPromotedItemRenderer : ITrackingParams
    {
        [JsonProperty("thumbnails")]
        public List<UrlImage> Thumbnails { get; set; }
        [JsonProperty("title")]
        public TextElement Title { get; set; }
        [JsonProperty("subtitle")]
        public TextElement Subtitle { get; set; }
        [JsonProperty("actionButton")]
        public Button ActionButton { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("dismissButton")]
        public Button DismissButton { get; set; }
        [JsonProperty("badge")]
        public Badge Badge { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("impressionEndpoints")]
        public List<ImpressionEndpoint> ImpressionEndpoints { get; set; }
    }
}