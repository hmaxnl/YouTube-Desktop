using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using YouTubeScrap.Util;

namespace YouTubeScrap.Models.Interfaces
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
        /// The label of the quality.
        /// </summary>
        /// 
        [JsonProperty("qualityLabel")]
        string QualityLabel { get; set; }
    }
}