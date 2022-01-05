using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class RichItemContent
    {
        [JsonProperty("videoRenderer")]
        public RichVideoContent RichVideoContent { get; set; }
        [JsonProperty("radioRenderer")]
        public RadioRenderer RadioRenderer { get; set; }
        [JsonProperty("displayAdRenderer")]
        public DisplayAdRenderer DisplayAdRenderer { get; set; }
    }
}