using Newtonsoft.Json;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class GuideDownloadsEntryRenderer
    {
        [JsonProperty("alwaysShow")]
        public bool AlwaysShow { get; set; }
        [JsonProperty("entryRenderer.guideEntryRenderer")]
        public GuideEntryRenderer EntryRenderer { get; set; }
    }
}