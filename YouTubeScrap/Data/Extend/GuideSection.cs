using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    public class GuideSection : IGuideSection
    {
        [JsonProperty("items")]
        [JsonConverter(typeof(JsonGuideConverter))]
        public List<object> Items { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("formattedTitle")]
        public TextElement FormattedTitle { get; set; } = new TextElement();
    }
}