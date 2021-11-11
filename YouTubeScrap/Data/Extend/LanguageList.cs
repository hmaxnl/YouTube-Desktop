using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class LanguageList
    {
        [JsonProperty("entries")]
        public List<DropdownItem> Entries { get; set; }
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
    }
}