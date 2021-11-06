using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class ColorData
    {
        [JsonProperty("backgroundColor")]
        public long BackgroundColor { get; set; }
        [JsonProperty("foregroundBodyColor")]
        public long ForegroundBodyColor { get; set; }
        [JsonProperty("foregroundActivatedColor")]
        public long ForegroundActivatedColor { get; set; }
        [JsonProperty("foregroundTitleColor")]
        public long ForegroundTitleColor { get; set; }
    }
}