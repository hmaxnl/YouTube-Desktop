using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace YouTubeScrap.Models
{
    public class Thumbnail
    {
        /// <summary>
        /// Thumbnail url.
        /// </summary>
        /// 
        [JsonProperty("url")]
        public string Url { get; set; }
        /// <summary>
        /// Thumbnail width.
        /// </summary>
        /// 
        [JsonProperty("width")]
        public long Width { get; set; }
        /// <summary>
        /// Thumbnail Height.
        /// </summary>
        /// 
        [JsonProperty("height")]
        public long Height { get; set; }
    }
}