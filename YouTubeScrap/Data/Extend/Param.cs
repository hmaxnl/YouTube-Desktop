using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public struct Param
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}