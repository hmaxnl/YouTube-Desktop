using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class RichThumbnail
    {
        [JsonProperty("thumbnails")]
        public List<UrlImage> Thumbnails { get; set; }
        [JsonProperty("enableHoveredLogging")]
        public bool EnableHoveredLogging { get; set; }
        [JsonProperty("enableOverlay")]
        public bool EnableOverlay { get; set; }
    }
}