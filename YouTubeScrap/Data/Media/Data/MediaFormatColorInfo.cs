using Newtonsoft.Json;

namespace YouTubeScrap.Data.Media.Data
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