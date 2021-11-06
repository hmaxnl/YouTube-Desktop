using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class ContinuationItemRenderer
    {
        [JsonProperty("trigger")]
        public string Trigger { get; set; }
        [JsonProperty("continuationEndpoint")]
        public ContinuationEndpoint Endpoint { get; set; }
        [JsonProperty("ghostCards")]
        public GhostCards GhostCards { get; set; }
    }
}