using Newtonsoft.Json;

namespace YouTubeScrap.Data.Media.Data
{
    public class CaptionAudioTrack
    {
        [JsonProperty("captionTrackIndices")]
        public int[] CaptionTrackIndices { get; set; }
        [JsonProperty("defaultCaptionTrackIndex")]
        public int DefaultCaptionTrackIndex { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        [JsonProperty("hasDefaultTrack")]
        public bool HasDefaultTrack { get; set; }
    }
}