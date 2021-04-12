using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IEndpoint
    {
        [JsonProperty("kind")]
        EndpointType Kind { get; set; }
    }
}