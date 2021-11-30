using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class GuideSection : ITrackingParams
    {
        [JsonProperty("items")]
        public List<GuideEntry> Items { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("formattedTitle")]
        public TextLabel FormattedTitle { get; set; }
    }
}