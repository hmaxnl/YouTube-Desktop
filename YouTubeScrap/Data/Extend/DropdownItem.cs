using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class DropdownItem
    {
        [JsonProperty("label")]
        public TextElement Label { get; set; }
        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }
        [JsonProperty("stringValue")]
        public string StringValue { get; set; }
        [JsonProperty("onSelectedCommand")]
        public Command OnSelectedCommand { get; set; }
    }
}