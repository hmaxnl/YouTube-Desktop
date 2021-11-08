using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        [JsonProperty("signalNavigationEndpoint")]
        public SignalNavigationEndpoint SignalNavigationEndpoint { get; set; }
        [JsonProperty("uploadEndpoint")]
        public UploadEndpoint UploadEndpoint { get; set; }
        
        [JsonProperty("continuationCommand")]
        public JObject ContinuationCommand { get; set; }
        [JsonProperty("signInEndpoint")]
        public JObject SignInEndpoint { get; set; }
    }
}