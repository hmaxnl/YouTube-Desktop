using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class MultiPageMenuRenderer : ITrackingParams
    {
        [JsonProperty("sections")]
        public List<MultiPageMenuSection> Sections { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
    }
}