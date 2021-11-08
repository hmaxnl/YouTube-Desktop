using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class MultiPageMenuSection
    {
        [JsonProperty("multiPageMenuSectionRenderer")]
        public MultiPageMenuSectionRenderer MenuSectionRenderer { get; set; }
    }
}