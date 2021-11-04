using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class GhostCards
    {
        [JsonProperty("ghostGridRenderer")]
        public GhostGridRenderer GhostGridRenderer { get; set; }
    }
}