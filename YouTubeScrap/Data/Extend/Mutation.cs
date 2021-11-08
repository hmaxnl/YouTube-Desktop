using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class Mutation
    {
        [JsonProperty("entityKey")]
        public string EntityKey { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("options")]
        public Options Options { get; set; }
    }

    public class Options
    {
        [JsonProperty("persistenceOption")]
        public string PersistenceOption { get; set; }
    }
}