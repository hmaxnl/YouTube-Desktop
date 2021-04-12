using System;
using Newtonsoft.Json;
using YouTubeScrap.Data.Media.Data;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IMediaFormat
    {
        /// <summary>
        /// The itag of the media stream.
        /// </summary>
        /// 
        [JsonProperty("itag")]
        int ITag { get; set; }
        /// <summary>
        /// The url of the media stream.
        /// </summary>
        /// 
        [JsonProperty("url")]
        string Url { get; set; }
        /// <summary>
        /// The codec and media type.
        /// </summary>
        /// 
        [JsonProperty("mimeType")]
        string MimeType { get; set; }
        /// <summary>
        /// The bitrate of the media stream.
        /// </summary>
        /// 
        [JsonProperty("bitrate")]
        long Bitrate { get; set; }
        /// <summary>
        /// Init range.
        /// </summary>
        /// 
        [JsonProperty("initRange")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        MediaFormatRange InitRange { get; set; }
        /// <summary>
        /// Index range.
        /// </summary>
        /// 
        [JsonProperty("indexRange")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        MediaFormatRange IndexRange { get; set; }
        /// <summary>
        /// Last time modified (in ms).
        /// </summary>
        /// 
        [JsonProperty("lastModified")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        DateTime LastModified { get; set; }
        /// <summary>
        /// The content length of the media stream.
        /// </summary>
        /// 
        [JsonProperty("contentLength")]
        long ContentLength { get; set; }
        /// <summary>
        /// The quality type.
        /// </summary>
        /// 
        [JsonProperty("quality")]
        string Quality { get; set; }
        /// <summary>
        /// The projection of the media stream.
        /// </summary>
        /// 
        [JsonProperty("projectionType")]
        string ProjectionType { get; set; }
        /// <summary>
        /// The average bitrate.
        /// </summary>
        /// 
        [JsonProperty("averageBitrate")]
        long AverageBitrate { get; set; }
        /// <summary>
        /// Target duration in seconds
        /// </summary>
        /// 
        [JsonProperty("targetDurationSec")]
        double TargetDurationSec { get; set; } // Mostly found on live video's.
        /// <summary>
        /// Max dvr duration in seconds.
        /// </summary>
        /// 
        [JsonProperty("maxDvrDurationSec")]
        double MaxDvrDurationSec { get; set; }  // Mostly found on live video's.
        /// <summary>
        /// A approx duration of the media stream.
        /// </summary>
        /// 
        [JsonProperty("approxDurationMs")]
        long ApproxDurationMs { get; set; }
        /// <summary>
        /// The signature cipher. (For videos that have protected urls. Like vevo content or validated channels.)
        /// </summary>
        /// 
        [JsonProperty("signatureCipher")]
        string SignatureCipher { get; set; }
    }
}