using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class searchEndpoint : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("searchEndpoint")]
        public JObject SearchEndpoint { get; set; }
    }
}