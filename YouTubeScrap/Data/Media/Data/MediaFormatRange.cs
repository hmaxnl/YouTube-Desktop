using Newtonsoft.Json;

namespace YouTubeScrap.Data.Media.Data
{
    public struct MediaFormatRange
    {
        [JsonProperty("start")]
        public int Start { get; set; }
        [JsonProperty("end")]
        public int End { get; set; }
    }
}