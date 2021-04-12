using Newtonsoft.Json;
using System;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class AddToPlaylistServiceEndpoint : IEndpoint
    {
        public EndpointType Kind { get; set; }
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
    }
}