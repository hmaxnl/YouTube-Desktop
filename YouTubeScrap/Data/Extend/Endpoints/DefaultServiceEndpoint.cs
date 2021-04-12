using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class DefaultServiceEndpoint : IEndpoint, IClickTrackingParams
    {
        public EndpointType Kind { get; set; }
        public string ClickTrackingParams { get; set; }
        [JsonProperty("commandMetadata")]
        public CommandMetadata CommandMetadata { get; set; }
        [JsonProperty("endpoint")]
        public IEndpoint Endpoint { get; set; }
    }
}