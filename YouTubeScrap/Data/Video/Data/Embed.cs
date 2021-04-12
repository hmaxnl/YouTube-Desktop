using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Video.Data
{
    public struct Embed
    {
        [JsonProperty("iframeUrl")]
        public string IframeUrl { get; set; }
        [JsonProperty("flashUrl")]
        public string FlashUrl { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("flashSecureUrl")]
        public string FlashSecureUrl { get; set; }
    }
}