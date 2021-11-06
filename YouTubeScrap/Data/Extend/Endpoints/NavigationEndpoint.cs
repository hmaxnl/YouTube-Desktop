using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class NavigationEndpoint : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("loggingUrls")]
        public string LoggingUrls { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("browseEndpoint")]
        public BrowseEndpoint BrowseEndpoint { get; set; }
        [JsonProperty("watchEndpoint")]
        public WatchEndpoint WatchEndpoint { get; set; }
        [JsonProperty("urlEndpoint")]
        public UrlEndpoint UrlEndpoint { get; set; }
        //TODO: Action implementation.
        /*Endpoint contents:
         - Endpoint
         - Endpoints (list of multiple endpoints.)
         - Command
         - */
    }
}