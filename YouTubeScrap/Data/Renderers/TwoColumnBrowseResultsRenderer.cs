using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class TwoColumnBrowseResultsRenderer
    {
        [JsonProperty("tabs")]
        public List<Tab> Tabs { get; set; }
    }
}