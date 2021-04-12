using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Data.Playlist
{
    public class PlaylistAction
    {
        [JsonProperty("addedVideoId")]
        public string AddedVideoId { get; set; }
        [JsonProperty("removedVideoId")]
        public string RemovedVideoId { get; set; }
        [JsonProperty("Action")]
        public string Action { get; set; }
    }
}