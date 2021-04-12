using System;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Media.Data
{
    public class CaptionMediaFormat : IMediaFormat, ICaptionMediaFormat
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
        public string XTags { get; set; }
        public string ProjectionType { get; set; }
        public AudioTrack AudioTrack { get; set; }
        public long AverageBitrate { get; set; }
        public double TargetDurationSec { get; set; }
        public double MaxDvrDurationSec { get; set; }
        public CaptionTrack CaptionTrack { get; set; }
        public long ApproxDurationMs { get; set; }
        public string SignatureCipher { get; set; }
    }
}