using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Feedback
{
    public class FeedbackButtons : IClickTrackingParams
    {
        [JsonProperty("buttons")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<ButtonRenderer> Buttons { get; set; }
        public string ClickTrackingParams { get; set; }
        [JsonProperty("dismissalViewStyle")]
        public string DismissalViewStyle { get; set; }
    }
}