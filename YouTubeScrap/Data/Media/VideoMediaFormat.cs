using System;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Media.Data;

namespace YouTubeScrap.Data.Media
{
    public class VideoMediaFormat : IMediaFormat, IVideoMediaFormat
    {
        public int ITag { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public long Bitrate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public MediaFormatRange InitRange { get; set; }
        public MediaFormatRange IndexRange { get; set; }
        public DateTime LastModified { get; set; }
        public long ContentLength { get; set; }
        public string Quality { get; set; }
        public int Framerate { get; set; }
        public string QualityLabel { get; set; }
        public string ProjectionType { get; set; }
        public long AverageBitrate { get; set; }
        public MediaFormatColorInfo ColorInfo { get; set; }
        public double TargetDurationSec { get; set; }
        public double MaxDvrDurationSec { get; set; }
        public long ApproxDurationMs { get; set; }
        public string SignatureCipher { get; set; }
    }
}