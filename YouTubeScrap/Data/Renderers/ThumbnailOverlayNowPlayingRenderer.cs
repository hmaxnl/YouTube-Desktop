using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ThumbnailOverlayNowPlayingRenderer
    {
        [JsonProperty("text.runs")]
        public List<TextRun> Text { get; set; }
    }
}