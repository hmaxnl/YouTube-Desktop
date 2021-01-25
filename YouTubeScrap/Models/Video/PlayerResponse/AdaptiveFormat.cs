using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YouTubeScrap.Models.Media;

namespace YouTubeScrap.Models.Video.PlayerResponse
{
    public class AdaptiveFormat
    {
        /// <summary>
        /// Video streams.
        /// </summary>
        /// 
        public List<VideoMediaFormat> VideoMedia { get; set; }
        /// <summary>
        /// Audio streams.
        /// </summary>
        /// 
        public List<AudioMediaFormat> AudioMedia { get; set; }
        /// <summary>
        /// Caption streams.
        /// </summary>
        /// 
        public List<CaptionMediaFormat> CaptionMedia { get; set; }
    }
}