using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class FrameworkUpdates
    {
        [JsonProperty("entityBatchUpdate")]
        public EntityBatchUpdate EntityBatchUpdate { get; set; }
    }

    public class EntityBatchUpdate
    {
        [JsonProperty("mutations")]
        public List<Mutation> Mutations { get; set; }
        [JsonProperty("timeStamp")]
        public TimeStamp TimeStamp { get; set; }
    }
}