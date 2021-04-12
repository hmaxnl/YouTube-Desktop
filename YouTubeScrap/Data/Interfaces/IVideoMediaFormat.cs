using Newtonsoft.Json;
using YouTubeScrap.Util;
using YouTubeScrap.Data.Media.Data;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IVideoMediaFormat
    {
        /// <summary>
        /// The width on the video.
        /// </summary>
        /// 
        [JsonProperty("width")]
        int Width { get; set; }
        /// <summary>
        /// The height of the video
        /// </summary>
        /// 
        [JsonProperty("height")]
        int Height { get; set; }
        /// <summary>
        /// The framerate.
        /// </summary>
        /// 
        [JsonProperty("fps")]
        int Framerate { get; set; }
        /// <summary>
        /// Quality label.
        /// </summary>
        /// 
        [JsonProperty("qualityLabel")]
        string QualityLabel { get; set; }
        /// <summary>
        /// Video color info
        /// </summary>
        /// 
        [JsonProperty("colorInfo")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        MediaFormatColorInfo ColorInfo { get; set; }
    }
}