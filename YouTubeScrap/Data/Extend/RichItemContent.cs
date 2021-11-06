using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data
{
    public class RichItemContent
    {
        [JsonProperty("videoRenderer")]
        public VideoRenderer VideoRenderer { get; set; }
        [JsonProperty("radioRenderer")]
        public RadioRenderer RadioRenderer { get; set; }
        [JsonProperty("displayAdRenderer")]
        public DisplayAdRenderer DisplayAdRenderer { get; set; }
    }

    public enum ContentKind
    {
        Video = 0,
        PlayList = 1,
        Channel = 2,
        Shelf = 3,
        Advertisement = 4,
        Unknown = 420
    }
}