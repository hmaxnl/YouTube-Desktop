using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class GhostCards
    {
        [JsonProperty("ghostGridRenderer.rows")]
        public int Rows { get; set; }
    }
}