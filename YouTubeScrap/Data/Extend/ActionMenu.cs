using Newtonsoft.Json;
using System.Collections.Generic;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ActionMenu : ITrackingParams
    {
        [JsonProperty("menuRenderer.items")]
        public List<ActionMenuItem> Items { get; set; }
        [JsonProperty("menuRenderer.actionMenuLabel")]
        public string ActionMenuLabel { get; set; }
        [JsonProperty("menuRenderer.accessibility")]
        public Accessibility Label { get; set; }
        [JsonProperty("menuRenderer.targetId")]
        public string TargetId { get; set; }
        [JsonProperty("menuRenderer.trackingParams")]
        public string TrackingParams { get; set; }
    }
}