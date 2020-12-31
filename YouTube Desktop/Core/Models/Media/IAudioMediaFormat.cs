using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models.Media
{
    public interface IAudioMediaFormat
    {
        /// <summary>
        /// Replication (Only on the mp4 codec!) needs check before extracted.
        /// </summary>
        /// 
        [JsonProperty("highReplication")]
        bool HighReplication { get; set; }
        /// <summary>
        /// Audio quality of the media stream.
        /// </summary>
        /// 
        [JsonProperty("audioQuality")]
        string AudioQuality { get; set; }
        /// <summary>
        /// the audio sample rate.
        /// </summary>
        /// 
        [JsonProperty("audioSampleRate")]
        long AudioSampleRate { get; set; }
        /// <summary>
        /// Total audio channels.
        /// </summary>
        /// 
        [JsonProperty("audioChannels")]
        int AudioChannels { get; set; }
        /// <summary>
        /// The loudness in Db.
        /// </summary>
        /// 
        [JsonProperty("loudnessDb")]
        double LoudnessDB { get; set; }
    }
}