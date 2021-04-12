using Newtonsoft.Json;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IHack
    {
        [JsonProperty("hack")]
        bool Hack { get; set; }
    }
}