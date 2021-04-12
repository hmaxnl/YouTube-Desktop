using Newtonsoft.Json;
using System;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public struct ReportFormEndPoint : IEndpoint
    {
        public EndpointType Kind { get; set; }
        [JsonProperty("params")]
        public string Params { get; set; }
    }
}