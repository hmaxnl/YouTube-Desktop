using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models.Media
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