using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ThumbnailOverlayEndorsementRenderer : ITrackingParams
    {
        [JsonProperty("text")]
        public TextLabel Text { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }
}