using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class GuideEntryData
    {
        [JsonProperty("guideEntryId")]
        public string GuideEntryId { get; set; }
    }
}