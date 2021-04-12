using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public class ButtonRenderer : ITrackingParams
    {
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("serviceEndpoint")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public ServiceEndpoint ServiceEndpoint { get; set; }

        public string TrackingParams { get; set; }
    }
}