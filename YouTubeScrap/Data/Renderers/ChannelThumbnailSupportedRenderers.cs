using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ChannelThumbnailSupportedRenderers
    {
        [JsonProperty("channelThumbnailWithLinkRenderer.thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("channelThumbnailWithLinkRenderer.navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("channelThumbnailWithLinkRenderer.accessibility")]
        public Accessibility Accessibility { get; set; }
    }
}