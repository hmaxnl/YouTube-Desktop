using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Renderers
{
    public class TwoColumnBrowseResultsRenderer
    {
        [JsonProperty("tabs")]
        public List<TabRenderer> Tabs { get; set; }
    }
}