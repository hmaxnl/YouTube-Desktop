using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Actions;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class ResponseReceivedAction : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("appendContinuationItemsAction")]
        public AppendContinuationItemsAction ContinuationItemsAction { get; set; }
    }
}