using Newtonsoft.Json;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IRichRenderer : ITrackingParams
    {
        [JsonProperty("content")]
        public RichItemContent RichItemContent { get; set; }
    }
}