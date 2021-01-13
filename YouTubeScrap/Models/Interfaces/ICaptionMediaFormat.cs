using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YouTubeScrap.Models.Caption;

namespace YouTubeScrap.Models.Interfaces
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