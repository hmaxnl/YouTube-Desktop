using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class PlaylistEditEndpoint
    {
        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
        [JsonProperty("actions")]
        public JArray Actions { get; set; }
    }
}