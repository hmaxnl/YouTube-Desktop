using System;
using System.Collections.Generic;
using System.Text;
using YouTubeScrap.Core;
using YouTubeScrap.Models.Video.PlayerResponse;

namespace YouTubeScrap.Models.Video
{
    public class VideoDataSnippet
    {

        public PlayabilityStatus PlayabilityStatus { get; set; }
        public StreamingData StreamingData { get; set; }
        public PlaybackTracking PlaybackTracking { get; set; }
        public VideoCaptions VideoCaptions { get; set; }
        public VideoDetails VideoDetails { get; set; }
        public Microformat Microformat { get; set; }
        public EndScreen EndScreen { get; set; }
        public PaidContent PaidContent { get; set; }
    }
}