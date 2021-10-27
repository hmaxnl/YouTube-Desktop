using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class ReflowOptions
    {
        [JsonProperty("minimumRowsOfVideosAtStart")]
        public int MinimumRowsOfVideosAtStart { get; set; }
        [JsonProperty("minimumRowsOfVideosBetweenSections")]
        public int MinimumRowsOfVideosBetweenSections { get; set; }
    }
}