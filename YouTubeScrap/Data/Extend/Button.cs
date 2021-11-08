using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class Button
    {
        [JsonProperty("buttonRenderer")]
        public ButtonRenderer ButtonRenderer { get; set; }
    }
}