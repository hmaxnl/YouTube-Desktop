using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class ContinuationEndpoint : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("continuationCommand")]
        public JObject Command { get; set; }
    }
}