using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class GuideBadge
    {
        [JsonProperty("liveBroadcasting")]
        public bool LiveBroadcasting { get; set; }
    }
}