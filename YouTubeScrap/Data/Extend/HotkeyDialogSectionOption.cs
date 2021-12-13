using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class HotkeyDialogSectionOption
    {
        [JsonProperty("label")]
        public TextElement Label { get; set; }
        [JsonProperty("hotkey")]
        public string Hotkey { get; set; }
    }
}