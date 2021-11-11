using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Extend.Actions;
using YouTubeScrap.Data.Extend.Commands;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Actions
{
    public class SignalAction : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        [JsonProperty("addToPlaylistCommand")]
        public AddToPlaylistCommand ToPlaylistCommand { get; set; }
        [JsonProperty("selectLanguageCommand")]
        public JObject SelectLanguageCommand { get; set; }
    }
}