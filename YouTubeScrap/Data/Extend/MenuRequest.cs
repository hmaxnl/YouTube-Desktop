using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class MenuRequest : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("signalServiceEndpoint")]
        public SignalServiceEndpoint SignalServiceEndpoint { get; set; }
    }
}