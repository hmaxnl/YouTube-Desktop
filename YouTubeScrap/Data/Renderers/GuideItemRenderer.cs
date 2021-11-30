using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Renderers
{
    public class GuideItemRenderer
    {
        [JsonProperty("guideSectionRenderer")]
        public GuideSection GuideSection { get; set; }
        [JsonProperty("guideSubscriptionsSectionRenderer")]
        public GuideSubscriptionSection GuideSubscriptionSection { get; set; }
    }
}