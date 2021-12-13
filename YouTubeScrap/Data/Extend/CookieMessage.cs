using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class CookieMessage
    {
        [JsonProperty("begin")]
        public TextElement Begin { get; set; }
        [JsonProperty("items")]
        public List<TextElement> Items { get; set; }
    }
}