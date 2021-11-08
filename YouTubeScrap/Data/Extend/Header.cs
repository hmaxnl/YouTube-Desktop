using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class Header
    {
        [JsonProperty("feedFilterChipBarRenderer")]
        public FeedFilterChipBar FeedFilterChipBar { get; set; }
        [JsonProperty("feedTabbedHeaderRenderer")]
        public FeedTabbedHeader FeedTabbedHeader { get; set; }
    }
}