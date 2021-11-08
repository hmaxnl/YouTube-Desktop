using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class FeedTabbedHeader
    {
        [JsonProperty("title")]
        public TextLabel Title { get; set; }
    }
}