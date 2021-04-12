using Newtonsoft.Json;

namespace YouTubeScrap.Data.Interfaces
{
    public interface ITrackingParams
    {
        /// <summary>
        /// For tracking!
        /// </summary>
        [JsonProperty("trackingParams")]
        string TrackingParams { get; set; }
    }
}