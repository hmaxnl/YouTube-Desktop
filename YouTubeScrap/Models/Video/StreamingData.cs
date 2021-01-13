using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Models.Video
{
    public class StreamingData
    {
        /// <summary>
        /// Data expires in seconds.
        /// </summary>
        /// 
        [JsonProperty("expiresInSeconds")]
        public long ExpiresInSeconds { get; set; }
    }
}