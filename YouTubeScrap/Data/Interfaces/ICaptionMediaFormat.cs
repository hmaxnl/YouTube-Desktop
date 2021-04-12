using Newtonsoft.Json;
using YouTubeScrap.Data.Media.Data;

namespace YouTubeScrap.Data.Interfaces
{
    public interface ICaptionMediaFormat
    {
        /// <summary>
        /// Xtags.
        /// </summary>
        /// 
        [JsonProperty("xtags")]
        string XTags { get; set; }
        /// <summary>
        /// Audio track.
        /// </summary>
        /// 
        [JsonProperty("audioTrack")]
        AudioTrack AudioTrack { get; set; }
        /// <summary>
        /// Caption track.
        /// </summary>
        /// 
        [JsonProperty("captionTrack")]
        CaptionTrack CaptionTrack { get; set; }
    }
}