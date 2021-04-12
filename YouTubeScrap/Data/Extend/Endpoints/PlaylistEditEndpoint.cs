using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Playlist;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class PlaylistEditEndpoint : IEndpoint
    {
        public EndpointType Kind { get; set; }
        [JsonProperty("playlistId")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string PlaylistId { get; set; }
        [JsonProperty("actions")]
        public List<PlaylistAction> Actions { get; set; }
    }
}