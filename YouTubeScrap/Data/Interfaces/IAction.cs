using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Actions;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IAction
    {
        [JsonProperty("kind")]
        ActionType Kind { get; set; }
    }
}