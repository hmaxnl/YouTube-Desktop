using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YouTube_Desktop.Core;

namespace YouTube_Desktop.Core.Models.Media
{
    public class VideoMediaFormat : IMediaFormat, IVideoMediaFormat
    {
        public int ITag { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public long Bitrate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [JsonProperty("initRange")] // Need converter
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public MediaFormatRange InitRange { get; set; }
        [JsonProperty("indexRange")] // Need converter
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public MediaFormatRange IndexRange { get; set; }
        public DateTime LastModified { get; set; }
        public long ContentLength { get; set; }
        public string Quality { get; set; }
        public int Framerate { get; set; }
        public string QualityLabel { get; set; }
        public string ProjectionType { get; set; }
        public long AverageBitrate { get; set; }
        [JsonProperty("colorInfo")] // Need converter
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public MediaFormatColorInfo ColorInfo { get; set; } // Not all video streams have a color info.
        public long ApproxDurationMs { get; set; }
        public string SignatureCipher { get; set; } // Not all video's has a cipher.
    }
}