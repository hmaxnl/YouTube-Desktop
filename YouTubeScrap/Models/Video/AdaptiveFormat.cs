using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Models.Video
{
    public class AdaptiveFormat
    {
        /// <summary>
        /// The time when the data expires in seconds
        /// </summary>
        /// 
        [JsonProperty("expiresInSeconds")]
        public long ExpiresInSeconds { get; set; }
        /// <summary>
        /// Dash manifest location url.
        /// </summary>
        /// 
        [JsonProperty("dashManifestUrl")]
        public string DashManifestUrl { get; set; }
        /// <summary>
        /// HLS manifest location url.
        /// </summary>
        /// 
        [JsonProperty("hlsManifestUrl")]
        public string HlsManifestUrl { get; set; }
    }
}