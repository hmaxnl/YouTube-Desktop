using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class BrowseEndpoint
    {
        [JsonProperty("browseId")]
        public string BrowseId { get; set; }
        [JsonProperty("canonicalBaseUrl")]
        public string CanonicalBaseUrl { get; set; }
    }
}