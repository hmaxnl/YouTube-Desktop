using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    public class GuideCollapsibleEntryRenderer
    {
        [JsonProperty("expanderItem")]
        public GuideEntryRenderer ExpanderItem { get; set; }

        [JsonProperty("collapserItem")]
        public GuideEntryRenderer CollapserItem { get; set; }
        
        [JsonProperty("expandableItems")]
        [JsonConverter(typeof(JsonGuideConverter))]
        public List<object> ExpandableItems { get; set; }
    }
}