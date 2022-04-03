using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class NotificationTopbarButtonRenderer : ITrackingParams
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("menuRequest")]
        public MenuRequest MenuRequest { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
        [JsonProperty("tooltip")]
        public string Tooltip { get; set; }
        [JsonProperty("updateUnseenCountEndpoint")]
        public UpdateUnseenCountEndpoint UpdateUnseenCountEndpoint { get; set; }
        [JsonProperty("notificationCount")]
        public int NotificationCount { get; set; }
        [JsonProperty("handlerDatas")]
        public List<string> HandlerDatas { get; set; }
    }
}