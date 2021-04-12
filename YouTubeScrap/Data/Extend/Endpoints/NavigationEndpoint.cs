using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class NavigationEndpoint : IEndpoint, IClickTrackingParams
    {
        public EndpointType Kind { get; set; }
        public string ClickTrackingParams { get; set; }
        [JsonProperty("loggingUrls")]
        public string LoggingUrls { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("endpoint")]
        public IEndpoint Endpoint { get; set; }
        //TODO: Action implementation.
    }
}