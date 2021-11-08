using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    public class RichShelfRenderer : ITrackingParams
    {
        [JsonProperty("title")]
        public TextLabel Title { get; set; }
        [JsonProperty("contents")]
        public List<ContentRender> Contents { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("showMoreButton")]
        public Button ShowMoreButton { get; set; }
    }
}