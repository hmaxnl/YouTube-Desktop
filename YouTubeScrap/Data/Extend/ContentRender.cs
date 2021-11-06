using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;
namespace YouTubeScrap.Data.Extend
{
    public class ContentRender
    {
        [JsonProperty("richItemRenderer")]
        public RichItemRenderer RichItem { get; set; }
        [JsonProperty("richSectionRenderer")]
        public RichSectionRenderer RichSection { get; set; }
        [JsonProperty("continuationItemRenderer")]
        public ContinuationItemRenderer ContinuationItem { get; set; }
    }
}