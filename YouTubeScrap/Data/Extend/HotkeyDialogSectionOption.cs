using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class HotkeyDialogSectionOption
    {
        [JsonProperty("label")]
        public TextLabel Label { get; set; }
        [JsonProperty("hotkey")]
        public string Hotkey { get; set; }
    }
}