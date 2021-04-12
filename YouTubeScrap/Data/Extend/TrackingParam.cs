using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class TrackingParam
    {
        [JsonProperty("service")]
        public string Service { get; set; }
        [JsonProperty("params")]
        public List<Param> Params { get; set; }
    }
}