using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YouTubeScrap.Data.Feedback;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class FeedbackEndpoint : IEndpoint
    {
        public EndpointType Kind { get; set; }
        [JsonProperty("feedbackToken")]
        public string FeedbackToken { get; set; }
        [JsonProperty("hideEnclosingContainer")]
        public bool HideEnclosingContainer { get; set; }
        [JsonProperty("feedbackActions")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<FeedbackButtons> FeedbackActions { get; set; }
    }
}