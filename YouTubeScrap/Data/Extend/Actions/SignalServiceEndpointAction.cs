using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Playlist;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend.Actions
{
    public class SignalServiceEndpointAction : IAction, IClickTrackingParams
    {
        public ActionType Kind { get; set; }
        public string ClickTrackingParams { get; set; }
        [JsonProperty("addToPlaylistCommand")]
        public AddToPlaylistCommand AddToPlaylistCommand { get; set; }
        [JsonProperty("action")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public IAction Action { get; set; }
    }
}