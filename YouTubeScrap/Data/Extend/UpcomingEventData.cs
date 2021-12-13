using System;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Extend
{
    public class UpcomingEventData
    {
        [JsonProperty("startTime")]
        public long StartTime { get; set; }// Unix epoch time
        [JsonProperty("isReminderSet")]
        public bool IsReminderSet { get; set; }
        [JsonProperty("upcomingEventText")]
        public TextElement UpcomingEventText { get; set; }

        public string GetDate => DateTimeOffset.FromUnixTimeSeconds(StartTime).DateTime.ToLocalTime().ToString();
    }
}