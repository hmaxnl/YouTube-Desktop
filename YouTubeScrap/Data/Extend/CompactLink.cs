using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class CompactLink
    {
        [JsonProperty("compactLinkRenderer")]
        public CompactLinkRenderer CompactLinkRenderer { get; set; }
    }
}