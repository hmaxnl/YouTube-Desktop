using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models
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