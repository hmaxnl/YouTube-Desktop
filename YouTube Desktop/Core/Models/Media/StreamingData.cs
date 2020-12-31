using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTube_Desktop.Core.Models.Video;

namespace YouTube_Desktop.Core.Models.Media
{
    public class StreamingData
    {
        /// <summary>
        /// The time when the data expires in seconds.
        /// </summary>
        /// 
        [JsonProperty("expiresInSeconds")]
        public long ExpiresInSeconds { get; set; }
        /// <summary>
        /// The audio/video mixxed media formats.
        /// </summary>
        /// 
        [JsonProperty("formats")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public List<MixxedMediaFormat> MixxedFormats { get; set; } // Needs converter
        /// <summary>
        /// The video and audio media.
        /// </summary>
        /// 
        [JsonProperty("adaptiveFormats")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public AdaptiveFormat AdaptiveFormats { get; set; } // Needs converter
    }
}