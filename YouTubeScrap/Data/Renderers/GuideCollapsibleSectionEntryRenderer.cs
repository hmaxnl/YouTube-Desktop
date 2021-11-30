using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class GuideCollapsibleSectionEntryRenderer
    {
        [JsonProperty("headerEntry.guideEntryRenderer")]
        public GuideEntryRenderer HeaderEntry { get; set; }
        [JsonProperty("expanderIcon")]
        public string ExpanderIcon { get; set; }
        [JsonProperty("collapserIcon")]
        public string CollapserIcon { get; set; }
        [JsonProperty("sectionItems")]
        public List<GuideEntry> SectionItems { get; set; }
        [JsonProperty("handlerDatas")]
        public string[] HandlerDatas { get; set; }
    }
}