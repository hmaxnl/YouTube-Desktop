using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data
{
    public class ResponseMetadata : ITrackingParams
    {
        [JsonProperty("responseContext")]
        public ResponseContext RespContext { get; set; }
        [JsonProperty("contents")]
        public Contents Contents { get; set; }
        [JsonProperty("header")]
        public Header Header { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("onResponseReceivedActions")]
        public List<ResponseReceivedAction> ResponseReceivedActions { get; set; }
        [JsonProperty("items")]
        public List<GuideItemRenderer> Items { get; set; }
        [JsonProperty("topbar")]
        public Topbar Topbar { get; set; }
        [JsonProperty("frameworkUpdates")]
        public FrameworkUpdates FrameworkUpdates { get; set; }
    }
}