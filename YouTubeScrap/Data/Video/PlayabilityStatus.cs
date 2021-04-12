using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Video
{
    public class PlayabilityStatus
    {
        [JsonProperty("status")]
        public VideoPlayabilityStatus Status { get; set; }
        [JsonProperty("playableInEmbed")]
        public bool PlayableInEmbed { get; set; }
        [JsonProperty("miniplayer")]
        public MiniplayerSetting Miniplayer { get; set; }
        [JsonProperty("contextParams")]
        public string ContextParams { get; set; }
    }
    public struct MiniplayerSetting
    {
        [JsonProperty("playbackMode")]
        public string PlaybackMode { get; set; }
    }
    public enum VideoPlayabilityStatus
    {
        OK,
        UNPLAYABLE,
        ERROR
    }
}