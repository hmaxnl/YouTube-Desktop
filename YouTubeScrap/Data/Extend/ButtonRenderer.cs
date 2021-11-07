using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ButtonRenderer : ITrackingParams
    {
        [JsonProperty("buttonRenderer.style")]
        public string Style { get; set; }
        [JsonProperty("buttonRenderer.text")]
        public List<TextRun> Text { get; set; }
        [JsonProperty("buttonRenderer.serviceEndpoint")]
        public ServiceEndpoint ServiceEndpoint { get; set; }
        [JsonProperty("buttonRenderer.trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("command")]
        public Command Command { get; set; }
    }
}