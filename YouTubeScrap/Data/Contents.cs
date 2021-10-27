using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data
{
    public class Contents
    {
        [JsonProperty("twoColumnBrowseResultsRenderer")]
        public TwoColumnBrowseResultsRenderer TwoColumnBrowseResultsRenderer { get; set; }
    }
}