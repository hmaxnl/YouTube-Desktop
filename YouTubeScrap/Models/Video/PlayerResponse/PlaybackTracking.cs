using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using YouTubeScrap.Util;

namespace YouTubeScrap.Models.Video.PlayerResponse
{
    public class PlaybackTracking
    {
        [JsonProperty("videostatsPlaybackUrl")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string VideoStatsPlaybackUrl { get; set; }
        [JsonProperty("videostatsDelayplayUrl")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string VideoStatsDelayplayUrl { get; set; }
        [JsonProperty("videostatsWatchtimeUrl")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string VideoStatsWatchtimeUrl { get; set; }
        [JsonProperty("ptrackingUrl")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string PTrackingUrl { get; set; }
        [JsonProperty("qoeUrl")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string QoeUrl { get; set; }
        [JsonProperty("setAwesomeUrl")]
        public UrlAttribute SetAwesomeUrl { get; set; }
        [JsonProperty("atrUrl")]
        public UrlAttribute AtrUrl { get; set; }
        [JsonProperty("youtubeRemarketingUrl")]
        public UrlAttribute YoutubeRemarketingUrl { get; set; }
        [JsonProperty("googleRemarketingUrl")]
        public UrlAttribute GoogleRemarketingUrl { get; set; }
    }
    public struct UrlAttribute
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
        [JsonProperty("elapsedMediaTimeSeconds")]
        public long ElapsedMediaTimeSeconds { get; set; }
    }
}