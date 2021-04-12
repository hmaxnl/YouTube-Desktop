using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    /// <summary>
    /// Main endpoint used for signals and commands.
    /// </summary>
    public class ServiceEndpoint : IEndpoint, IClickTrackingParams
    {
        public EndpointType Kind { get; set; }
        public string ClickTrackingParams { get; set; }
        [JsonProperty("loggingUrls")]
        public string LoggingUrls { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("endpoint")]
        public IEndpoint Endpoint { get; set; }
    }
}