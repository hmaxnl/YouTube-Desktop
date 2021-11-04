using Newtonsoft.Json;

namespace YouTubeScrap.Data.Renderers
{
    public class GhostGridRenderer
    {
        [JsonProperty("rows")]
        public int Rows { get; set; }
    }
}