using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class FeedTabbedHeader
    {
        [JsonProperty("title")]
        public TextElement Title { get; set; }
    }
}