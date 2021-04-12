using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class ThumbnailSet
    {
        [JsonIgnore]
        private readonly string videoId;
        public ThumbnailSet(string vId)
        {
            videoId = vId;
        }
        [JsonProperty("lowResUrl")]
        public string LowResUrl => $"https://img.youtube.com/vi/{videoId}/default.jpg";
        [JsonProperty("mediumResUrl")]
        public string MediumResUrl => $"https://img.youtube.com/vi/{videoId}/mqdefault.jpg";
        [JsonProperty("highResUrl")]
        public string HighResUrl => $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";
        [JsonProperty("standardResUrl")]
        public string StandardResUrl => $"https://img.youtube.com/vi/{videoId}/sddefault.jpg";
        [JsonProperty("maxResUrl")]
        public string MaxResUrl => $"https://img.youtube.com/vi/{videoId}/maxresdefault.jpg";
    }
}