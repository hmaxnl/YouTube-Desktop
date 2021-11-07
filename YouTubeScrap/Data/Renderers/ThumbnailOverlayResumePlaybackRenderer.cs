using Newtonsoft.Json;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayResumePlaybackRenderer
    {
        [JsonProperty("percentDurationWatched")]
        public int PercentDurationWatched { get; set; }
    }
}