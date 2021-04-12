using Newtonsoft.Json;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IClickTrackingParams
    {
        [JsonProperty("clickTrackingParams")]
        string ClickTrackingParams { get; set; }
    }
}