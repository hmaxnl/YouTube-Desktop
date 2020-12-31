using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models.Media
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