using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class SignalNavigationEndpoint
    {
        [JsonProperty("signal")]
        public string Signal { get; set; }
    }
}