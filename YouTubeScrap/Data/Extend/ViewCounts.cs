using Newtonsoft.Json;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public struct ViewCounts
    {
        [JsonProperty("viewCount")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string ViewCount { get; set; }
        [JsonProperty("shortViewCount")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string ShortViewCount { get; set; }
    }
}