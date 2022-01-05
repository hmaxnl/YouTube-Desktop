using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    public class RichItemRenderer : ITrackingParams
    {
        [JsonProperty("content")]
        [JsonConverter(typeof(JsonContentConverter))]
        public object Content { get; set; }
        public string TrackingParams { get; set; }
    }
}