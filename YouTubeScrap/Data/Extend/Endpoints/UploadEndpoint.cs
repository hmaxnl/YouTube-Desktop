using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class UploadEndpoint
    {
        [JsonProperty("hack")]
        public bool Hack { get; set; }
    }
}