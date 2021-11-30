using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class GuideSubscriptionSection : ITrackingParams
    {
        [JsonProperty("sort")]
        public string Sort { get; set; }
        [JsonProperty("items")]
        public List<GuideEntry> Items { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("formattedTitle")]
        public TextLabel FormattedTitle { get; set; }
        [JsonProperty("handlerDatas")]
        public string[] HandlerDatas { get; set; }
    }
}