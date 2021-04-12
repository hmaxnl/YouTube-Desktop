using System;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class Endpoint : IEndpoint, IClickTrackingParams
    {
        public EndpointType Kind { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("endpoint")]
        public IEndpoint IEndpoint { get; set; }
        public string ClickTrackingParams { get; set; }
        
    }
}