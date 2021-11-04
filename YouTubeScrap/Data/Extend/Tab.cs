using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class Tab
    {
        [JsonProperty("tabRenderer")]
        public TabRenderer TabRenderer { get; set; }
    }
}