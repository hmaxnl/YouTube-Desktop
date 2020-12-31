using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models.Media
{
    public class MixxedMediaFormat : IMediaFormat, IVideoMediaFormat, IAudioMediaFormat
    {
        public int ITag { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public long Bitrate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime LastModified { get; set; }
        public long ContentLength { get; set; }
        public string Quality { get; set; }
        public int Framerate { get; set; }
        public string QualityLabel { get; set; }
        public string ProjectionType { get; set; }
        public long AverageBitrate { get; set; }
        public string AudioQuality { get; set; }
        public long ApproxDurationMs { get; set; }
        public long AudioSampleRate { get; set; }
        public int AudioChannels { get; set; }
        public bool HighReplication { get; set; } // Not used in the mixxed format.
        public double LoudnessDB { get; set; } // Not used in the mixxed format.
        public string SignatureCipher { get; set; }
    }
}