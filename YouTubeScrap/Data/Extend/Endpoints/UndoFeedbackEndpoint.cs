using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Feedback;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class UndoFeedbackEndpoint : IEndpoint
    {
        public EndpointType Kind { get; set; }
        [JsonProperty("undoToken")]
        public string UndoToken { get; set; }
        [JsonProperty("actions")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<FeedbackAction> Actions { get; set; }
    }
}