using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class FeedbackEndpoint
    {
        [JsonProperty("feedbackToken")]
        public string FeedbackToken { get; set; }
        [JsonProperty("uiActions.hideEnclosingContainer")]
        public bool HideEnclosingContainer { get; set; }
        [JsonProperty("actions")]
        public JArray FeedbackActions { get; set; }
    }
}