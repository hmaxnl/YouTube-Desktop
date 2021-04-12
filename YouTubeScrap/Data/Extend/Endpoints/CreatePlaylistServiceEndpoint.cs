using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class CreatePlaylistServiceEndpoint
    {
        [JsonProperty("videoIds")]
        public string[] VideoIds { get; set; }
        [JsonProperty("params")]
        public string Params { get; set; }
    }
}