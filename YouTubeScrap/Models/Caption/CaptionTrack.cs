using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using YouTubeScrap.Util;

namespace YouTubeScrap.Models.Caption
{
    public class CaptionTrack
    {
        /// <summary>
        /// Caption display name.
        /// </summary>
        /// 
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        /// <summary>
        /// Caption vss id.
        /// </summary>
        /// 
        [JsonProperty("vssId")]
        public string VssId { get; set; }
        /// <summary>
        /// Caption language code.
        /// </summary>
        /// 
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }
        /// <summary>
        /// Kind.
        /// </summary>
        /// 
        [JsonProperty("kind")]
        public string Kind { get; set; }
        /// <summary>
        /// The url to the caption (In XML Format).
        /// </summary>
        /// 
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
        /// <summary>
        /// The name of the caption.
        /// </summary>
        /// 
        [JsonProperty("name")]
        [JsonConverter(typeof(JsonDeserialConverter))]
        public string CaptionName { get; set; }
        /// <summary>
        /// Is caption translatable.
        /// </summary>
        /// 
        [JsonProperty("isTranslatable")]
        public bool IsTranslatable { get; set; }
    }
}