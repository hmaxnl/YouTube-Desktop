using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class WatchEndpoint
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
        [JsonProperty("params")]
        public string Params { get; set; }
        [JsonProperty("playerParams")]
        public string PlayerParams { get; set; }
        [JsonProperty("playerExtraUrlParams")]
        public List<Param> PlayerExtraUrlParams { get; set; }
        [JsonProperty("continuePlayback")]
        public bool ContinuePlayback { get; set; }
        [JsonProperty("loggingContext.vssLoggingContext.serializedContextData")]
        public string LoggingContext { get; set; }
        [JsonProperty("startTimeSeconds")]
        public long StartTimeSeconds { get; set; }
        [JsonProperty("watchEndpointSupportedOnesieConfig.html5PlaybackOnesieConfig.commonConfig.url")]
        public string WatchEndpointSupportedOnesieConfig { get; set; }
    }
}