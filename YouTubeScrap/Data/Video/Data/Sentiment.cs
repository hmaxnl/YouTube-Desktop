using Newtonsoft.Json;

namespace YouTubeScrap.Data.Video.Data
{
    public struct Sentiment
    {
        [JsonProperty("percentIfIndifferent")]
        public short PercentIfIndiffrent { get; set; }
        [JsonProperty("percentIfLiked")]
        public short PercentIfLiked { get; set; }
        [JsonProperty("percentIfDisliked")]
        public short PercentIfDisliked { get; set; }
        [JsonProperty("likeStatus")]
        public string LikeStatus { get; set; }
        [JsonProperty("tooltip")]
        public string ToolTip { get; set; }
    }
}