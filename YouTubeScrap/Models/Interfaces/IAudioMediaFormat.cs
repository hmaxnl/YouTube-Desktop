using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using YouTubeScrap.Util;

namespace YouTubeScrap.Models.Interfaces
{
    public interface IAudioMediaFormat
    {
        /// <summary>
        /// Replication (Only on the mp4 codec!).
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