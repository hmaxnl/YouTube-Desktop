using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public class ActionMenuItem : ITrackingParams
    {
        [JsonProperty("text")]
        public List<TextRun> Text { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("iconType")]
        public string IconType { get; set; }
        [JsonProperty("serviceEndpoint")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public ServiceEndpoint ServiceEndpoint { get; set; }

        public string TrackingParams { get; set; }
    }
}