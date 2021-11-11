using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class CookieMessage
    {
        [JsonProperty("begin")]
        public TextLabel Begin { get; set; }
        [JsonProperty("items")]
        public List<TextLabel> Items { get; set; }
    }
}