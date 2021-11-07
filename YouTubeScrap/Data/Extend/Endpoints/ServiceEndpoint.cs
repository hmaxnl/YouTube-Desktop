using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    /// <summary>
    /// Main endpoint used for signals and commands.
    /// </summary>
    public class ServiceEndpoint : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("loggingUrls")]
        public string LoggingUrls { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        
        [JsonProperty("signalServiceEndpoint")]
        public SignalServiceEndpoint SignalServiceEndpoint { get; set; }
        [JsonProperty("playlistEditEndpoint")]
        public PlaylistEditEndpoint PlaylistEditEndpoint { get; set; }
        [JsonProperty("addToPlaylistServiceEndpoint")]
        public AddToPlaylistServiceEndpoint AddToPlaylistServiceEndpoint { get; set; }
        [JsonProperty("feedbackEndpoint")]
        public FeedbackEndpoint FeedbackEndpoint { get; set; }
        [JsonProperty("undoFeedbackEndpoint")]
        public UndoFeedbackEndpoint UndoFeedbackEndpoint { get; set; }
        
        [JsonProperty("getReportFormEndpoint")]
        public JObject GetReportFormEndpoint { get; set; }
    }
}