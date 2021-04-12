using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Channel
{
    public class ChannelVideoResult
    {
        [JsonProperty("thumbnails")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("navigationEndpoint")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("label")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string Label { get; set; }
    }
}