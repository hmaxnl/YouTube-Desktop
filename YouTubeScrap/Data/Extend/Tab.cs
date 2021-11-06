using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Tab
    {
        [JsonProperty("tabRenderer.selected")]
        public bool Selected { get; set; }
        [JsonProperty("tabRenderer.content.richGridRenderer")]
        public RichGrid Content { get; set; }
        [JsonProperty("tabRenderer.tabIdentifier")]
        public string TabIdentifier { get; set; }
        [JsonProperty("tabRenderer.trackingParams")]
        public string TrackingParams { get; set; }
    }
}