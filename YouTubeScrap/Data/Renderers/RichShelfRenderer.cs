using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class RichShelfRenderer : ITrackingParams
    {
        [JsonProperty("title")]
        public TextRun Title { get; set; }
        [JsonProperty("contents")]
        public List<ContentRender> Contents { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        //TODO: add showMoreButton property
    }
}