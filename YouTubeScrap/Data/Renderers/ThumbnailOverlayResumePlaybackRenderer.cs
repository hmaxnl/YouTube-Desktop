using Newtonsoft.Json;

namespace YouTubeScrap.Data.Renderers
{
    public class ThumbnailOverlayResumePlaybackRenderer
    {
        [JsonProperty("percentDurationWatched")]
        public double PercentDurationWatched { get; set; }
    }
}