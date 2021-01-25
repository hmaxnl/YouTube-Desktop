using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

using Newtonsoft.Json;

using YouTubeScrap.Util;

namespace YouTubeScrap.Models.Video.PlayerResponse
{
    public class PaidContent
    {
        [JsonProperty("text")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string Text { get; set; }
        [JsonProperty("durationMs")]
        public long DurationMs { get; set; }
        public bool IsPaid { get { return !string.IsNullOrEmpty(Text); } }
    }
}