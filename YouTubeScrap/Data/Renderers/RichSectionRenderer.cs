using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    public class RichSectionRenderer : ITrackingParams
    {
        [JsonProperty("content")]
        [JsonConverter(typeof(JsonContentConverter))]
        public object Content { get; set; }
        public string TrackingParams { get; set; }
    }
}