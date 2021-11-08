using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class HotkeyDialog : ITrackingParams
    {
        [JsonProperty("title")]
        public TextLabel Title { get; set; }
        [JsonProperty("sections")]
        public List<HotkeyDialogSection> Sections { get; set; }
        [JsonProperty("dismissButton")]
        public Button DismissButton { get; set; }
        public string TrackingParams { get; set; }
    }
}