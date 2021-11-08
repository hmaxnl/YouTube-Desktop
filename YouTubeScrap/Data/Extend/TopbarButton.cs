using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class TopbarButton
    {
        [JsonProperty("topbarMenuButtonRenderer")]
        public MenuButtonRenderer MenuButtonRenderer { get; set; }
        [JsonProperty("buttonRenderer")]
        public ButtonRenderer ButtonRenderer { get; set; }
    }
}