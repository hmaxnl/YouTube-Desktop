using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace YouTubeScrap.Models.Video.PlayerResponse
{
    public struct MediaFormatColorInfo
    {
        [JsonProperty("primaries")]
        public string Primaries { get; set; }
        [JsonProperty("transferCharacteristics")]
        public string TransferCharacteristics { get; set; }
        [JsonProperty("matrixCoefficients")]
        public string MatrixCoefficients { get; set; }
    }
}