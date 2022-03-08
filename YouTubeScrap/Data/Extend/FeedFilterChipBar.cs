using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class FeedFilterChipBar : ITrackingParams
    {
        public string TrackingParams { get; set; }
        [JsonProperty("contents")]
        public List<ContentRender> Contents { get; set; }
        [JsonProperty("nextButton")]
        public ButtonRenderer NextButtonRenderer { get; set; }
        [JsonProperty("previousButton")]
        public ButtonRenderer PreviousButtonRenderer { get; set; }
    }
}