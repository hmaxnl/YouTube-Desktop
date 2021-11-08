using Newtonsoft.Json;

namespace YouTubeScrap.Data.Renderers
{
    public class MenuRenderer
    {
        [JsonProperty("multiPageMenuRenderer")]
        public MultiPageMenuRenderer MultiPageMenuRenderer { get; set; }
    }
}