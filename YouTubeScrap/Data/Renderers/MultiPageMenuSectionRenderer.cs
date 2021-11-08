using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class MultiPageMenuSectionRenderer : ITrackingParams
    {
        [JsonProperty("items")]
        public List<CompactLink> Items { get; set; }
        public string TrackingParams { get; set; }
    }
}