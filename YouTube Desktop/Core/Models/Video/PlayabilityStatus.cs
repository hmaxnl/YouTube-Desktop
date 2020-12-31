using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models.Video
{
    /// <summary>
    /// For Json serialization.
    /// </summary>
    public class PlayabilityStatus
    {
        /// <summary>
        /// Status code.
        /// </summary>
        /// 
        [JsonProperty("status")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public VideoStatus Status { get; set; }
        /// <summary>
        /// The reason why the video is not available.
        /// </summary>
        /// 
        [JsonProperty("reason")]
        public string Reason { get; set; }
        /// <summary>
        /// The sub reason.
        /// </summary>
        /// 
        [JsonProperty("errorScreen")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public string SubReason { get; set; }
        /// <summary>
        /// Can play in embedded.
        /// </summary>
        /// 
        [JsonProperty("playableInEmbed")]
        public bool PlayableInEmbed { get; set; }
        /// <summary>
        /// Context parameters.
        /// </summary>
        /// 
        [JsonProperty("contextParams")]
        public string ContextParams { get; set; }
    }
    public enum VideoStatus
    {
        OK = 0,
        UNPLAYABLE = 1,
        ERROR = 2
    }
}