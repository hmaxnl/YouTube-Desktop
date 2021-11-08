using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class MultiActionRenderer : ITrackingParams
    {
        [JsonProperty("responseText")]
        public TextLabel ResponseText { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("buttons")]
        public List<Button> Buttons { get; set; }
        [JsonProperty("dismissalViewStyle")]
        public string DismissalViewStyle { get; set; }
    }
}