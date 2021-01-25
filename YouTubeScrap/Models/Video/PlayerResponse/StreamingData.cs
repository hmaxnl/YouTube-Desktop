using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using YouTubeScrap.Util;
using YouTubeScrap.Models.Media;

namespace YouTubeScrap.Models.Video.PlayerResponse
{
    public class StreamingData
    {
        /// <summary>
        /// Data expires in seconds.
        /// </summary>
        /// 
        [JsonProperty("expiresInSeconds")]
        public long ExpiresInSeconds { get; set; }
        /// <summary>
        /// Audio/Video mixxed streams (Max resolution = 720p-30fps)
        /// </summary>
        /// 
        [JsonProperty("formats")]
        public List<MixxedMediaFormat> MixxedFormats { get; set; }
        /// <summary>
        /// The adaptive format streams.
        /// </summary>
        /// 
        [JsonProperty("adaptiveFormats")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public AdaptiveFormat AdaptiveFormat { get; set; }
        /// <summary>
        /// Dash manifest location url. (For live content.)
        /// </summary>
        /// 
        [JsonProperty("dashManifestUrl")]
        public string DashManifestUrl { get; set; }
        /// <summary>
        /// HLS manifest location url. (For live content.)
        /// </summary>
        /// 
        [JsonProperty("hlsManifestUrl")]
        public string HlsManifestUrl { get; set; }
        /// <summary>
        /// A probe url (Even i don't now what this is for.)
        /// </summary>
        /// 
        [JsonProperty("probeUrl")]
        public string ProbeUrl { get; set; }
    }
}