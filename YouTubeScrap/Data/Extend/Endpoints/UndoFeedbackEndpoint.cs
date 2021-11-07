using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class UndoFeedbackEndpoint
    {
        [JsonProperty("undoToken")]
        public string UndoToken { get; set; }
        [JsonProperty("actions")]
        public JArray Actions { get; set; }
    }
}