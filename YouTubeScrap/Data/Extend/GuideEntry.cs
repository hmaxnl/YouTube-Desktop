using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class GuideEntry
    {
        [JsonProperty("guideEntryRenderer")]
        public GuideEntryRenderer GuideEntryRenderer { get; set; }
        [JsonProperty("guideCollapsibleEntryRenderer")]
        public GuideCollapsibleEntryRenderer GuideCollapsibleEntryRenderer { get; set; }
        [JsonProperty("guideCollapsibleSectionEntryRenderer")]
        public GuideCollapsibleSectionEntryRenderer GuideCollapsibleSectionEntryRenderer { get; set; }
        [JsonProperty("guideDownloadsEntryRenderer")]
        public GuideDownloadsEntryRenderer GuideDownloadsEntryRenderer { get; set; }
    }
}