using Newtonsoft.Json;
using YouTubeScrap.Data.Video;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IPlayabilityStatus
    {
        /// <summary>
        /// Video status code.
        /// </summary>
        /// 
        [JsonProperty("status")]
        VideoPlayabilityStatus VideoStatus { get; set; }
        /// <summary>
        /// The reason why the video is unavailable.
        /// </summary>
        /// 
        [JsonProperty("reason")]
        string UnvailabilityReason { get; set; }
        /// <summary>
        /// Error information for unvailability of the video.
        /// </summary>
        /// 
        [JsonProperty("errorScreen")]
        string ErrorReason { get; set; }
        /// <summary>
        /// If the video is playable in embedded mode.
        /// </summary>
        /// 
        [JsonProperty("playableInEmbed")]
        bool PlayableInEmbed { get; set; }
        /// <summary>
        /// Some context params.
        /// </summary>
        /// 
        [JsonProperty("contextParams")]
        string ContextParams { get; set; }
    }
}