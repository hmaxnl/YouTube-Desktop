using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}