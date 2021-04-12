using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public struct LikeCounts
    {
        [JsonProperty("likeCount")]
        public string LikeCount { get; set; }
        [JsonProperty("shortLikeCount")]
        public string ShortLikeCount { get; set; }
    }
}