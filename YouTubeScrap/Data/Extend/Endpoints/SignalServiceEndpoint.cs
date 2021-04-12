using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Actions;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class SignalServiceEndpoint : IEndpoint
    {
        public EndpointType Kind { get; set; }
        [JsonProperty("signal")]
        public string Signal { get; set; }
        [JsonProperty("actions")]
        public List<SignalServiceEndpointAction> Actions { get; set; }
    }
}