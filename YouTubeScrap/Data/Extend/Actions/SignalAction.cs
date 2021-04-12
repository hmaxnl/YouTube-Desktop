using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Actions;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class SignalAction : IAction
    {
        public ActionType Kind { get; set; }
        [JsonProperty("signal")]
        public string Signal { get; set; }
    }
}