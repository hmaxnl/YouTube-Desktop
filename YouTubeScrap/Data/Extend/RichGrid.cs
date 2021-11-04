using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class RichGrid
    {
        [JsonProperty("richGridRenderer")]
        public RichGridRenderer RichGridRenderer { get; set; }
    }
}