using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class ToggledServiceEndpoint : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("playlistEditEndpoint")]
        public JObject PlaylistEditEndpoint { get; set; }
        [JsonProperty("signalServiceEndpoint")]
        public SignalServiceEndpoint SignalServiceEndpoint { get; set; }
    }
}