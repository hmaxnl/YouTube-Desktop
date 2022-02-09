using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class InlinePlaybackEndpoint : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("watchEndpoint")]
        public WatchEndpoint WatchEndpoint { get; set; }
    }
}