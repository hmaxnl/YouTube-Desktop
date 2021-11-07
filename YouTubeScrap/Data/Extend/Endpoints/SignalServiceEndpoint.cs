using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Extend.Actions;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend.Endpoints
{
    public class SignalServiceEndpoint
    {
        [JsonProperty("signal")]
        public string Signal { get; set; }
        [JsonProperty("actions")]
        public JArray Actions { get; set; }
    }
}