using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayHoverTextRenderer
    {
        [JsonProperty("text.runs")]
        public List<TextRun> Text { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}