using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    public class GuideCollapsibleSectionEntryRenderer
    {
        [JsonProperty("headerEntry")]
        public GuideEntryRenderer HeaderEntry { get; set; }
        [JsonProperty("expanderIcon")]
        public string ExpanderIcon { get; set; }
        [JsonProperty("collapserIcon")]
        public string CollapserIcon { get; set; }
        [JsonProperty("sectionItems")]
        [JsonConverter(typeof(JsonGuideConverter))]
        public List<object> SectionItems { get; set; }
        [JsonProperty("handlerDatas")]
        public string[] HandlerDatas { get; set; }
    }
}