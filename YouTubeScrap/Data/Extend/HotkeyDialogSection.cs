using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class HotkeyDialogSection
    {
        [JsonProperty("title")]
        public TextElement Title { get; set; }
        [JsonProperty("options")]
        public List<HotkeyDialogSectionOption> Options { get; set; }
    }
}