using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend.Actions
{
    public class AppendContinuationItemsAction
    {
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
        [JsonProperty("continuationItems")]
        [JsonConverter(typeof(JsonContentConverter))]
        public List<object> ContinuationItems { get; set; }
    }
}