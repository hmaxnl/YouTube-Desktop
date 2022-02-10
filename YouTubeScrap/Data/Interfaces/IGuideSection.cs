using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IGuideSection : ITrackingParams
    {
        [JsonProperty("items")]
        public List<object> Items { get; set; }
        [JsonProperty("formattedTitle")]
        public TextElement FormattedTitle { get; set; }
    }
}