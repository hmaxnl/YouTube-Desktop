using Newtonsoft.Json;

namespace YouTubeScrap.Data.Playlist
{
    public class AddToPlaylistCommand
    {
        [JsonProperty("openMiniplayer")]
        public bool OpenMiniplayer { get; set; }
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
        [JsonProperty("listType")]
        public string ListType { get; set; }
        [JsonProperty("onCreateListCommand")]
        public CreateListCommand CreateListCommand { get; set; }
        [JsonProperty("videoIds")]
        public string[] VideoIds { get; set; }
    }
}