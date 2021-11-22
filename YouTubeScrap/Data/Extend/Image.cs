using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("width")]
        public long Width { get; set; }
        [JsonProperty("height")]
        public long Height { get; set; }

        public ImageQuality Quality
        {
            get
            {
                switch (Width)
                {
                    case <= 360:
                        return ImageQuality.Low;
                    case <= 720:
                        return ImageQuality.Medium;
                    case <= 1080:
                        return ImageQuality.High;
                    case > 1080:
                        return ImageQuality.UltraHigh;
                }
            }
        }
    }

    public enum ImageQuality
    {
        Low,
        Medium,
        High,
        UltraHigh
    }
}