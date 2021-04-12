using Newtonsoft.Json;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IResponseContext
    {
        [JsonProperty("responseContext")]
        ResponseContext ResponseContext { get; set; }
    }
}