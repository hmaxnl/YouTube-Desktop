using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Media;

namespace YouTubeScrap.Data.Video
{
    public class StreamingData
    {
        [JsonProperty("expiresInSeconds")]
        public string ExpiresInSeconds { get; set; }
        [JsonProperty("expires")]
        public string Expires { get; set; }
        [JsonProperty("mixxedFormats")]
        public List<MixxedMediaFormat> MixxedFormats { get; set; }
        [JsonProperty("videoFormats")]
        public List<VideoMediaFormat> VideoFormats { get; set; }
        [JsonProperty("audioFormats")]
        public List<AudioMediaFormat> AudioFormats { get; set; }
    }
}