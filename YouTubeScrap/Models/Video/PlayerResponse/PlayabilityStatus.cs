using System;
using System.Collections.Generic;
using System.Text;

using YouTubeScrap.Models.Interfaces;

namespace YouTubeScrap.Models.Video.PlayerResponse
{
    public class PlayabilityStatus : IPlayabilityStatus
    {
        public VideoPlayabilityStatus VideoStatus { get; set; }
        public string UnvailabilityReason { get; set; }
        public string ErrorReason { get; set; }
        public bool PlayableInEmbed { get; set; }
        public string ContextParams { get; set; }
    }
    public enum VideoPlayabilityStatus
    {
        OK = 0,
        UNPLAYABLE = 1,
        ERROR = 2
    }
}