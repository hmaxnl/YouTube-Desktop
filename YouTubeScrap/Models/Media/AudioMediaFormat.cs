using System;
using System.Collections.Generic;
using System.Text;

using YouTubeScrap.Models.Interfaces;
using YouTubeScrap.Models.Video.PlayerResponse;

namespace YouTubeScrap.Models.Media
{
    public class AudioMediaFormat : IMediaFormat, IAudioMediaFormat
    {
        public int ITag { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public long Bitrate { get; set; }
        public MediaFormatRange InitRange { get; set; }
        public MediaFormatRange IndexRange { get; set; }
        public DateTime LastModified { get; set; }
        public long ContentLength { get; set; }
        public string Quality { get; set; }
        public string ProjectionType { get; set; }
        public long AverageBitrate { get; set; }
        public string AudioQuality { get; set; }
        public double TargetDurationSec { get; set; }
        public double MaxDvrDurationSec { get; set; }
        public long ApproxDurationMs { get; set; }
        public long AudioSampleRate { get; set; }
        public int AudioChannels { get; set; }
        public double LoudnessDB { get; set; }
        public string SignatureCipher { get; set; }
        public bool HighReplication { get; set; }
    }
}