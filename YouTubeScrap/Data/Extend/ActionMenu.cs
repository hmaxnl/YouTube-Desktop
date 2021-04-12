using Newtonsoft.Json;
using System.Collections.Generic;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public class ActionMenu : ITrackingParams
    {
        [JsonProperty("items")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<ActionMenuItem> Items { get; set; }
        [JsonProperty("actionMenuLabel")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string ActionMenuLabel { get; set; }
        [JsonProperty("targetId")]
        public string TargedId { get; set; }

        public string TrackingParams { get; set; }
    }
}