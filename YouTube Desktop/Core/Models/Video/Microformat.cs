using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models.Video
{
    public class Microformat
    {
        [JsonProperty("thumbnail")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("title")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public string Title { get; set; }
        [JsonProperty("description")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public string Desciption { get; set; }
        [JsonProperty("lengthSeconds")]
        public long LengthInSeconds { get; set; }
        [JsonProperty("ownerProfileUrl")]
        public string OwnerProfileUrl { get; set; }
        [JsonProperty("externalChannelId")]
        public string ExternalChannelId { get; set; }
        [JsonProperty("availableCountries")]
        public List<string> AvailableCountries { get; set; }
        [JsonProperty("isUnlisted")]
        public bool IsUnlisted { get; set; }
        [JsonProperty("hasYpcMetadata")]
        public bool HasYpcMetadata { get; set; }
        [JsonProperty("viewCount")]
        public long ViewCount { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("publishDate")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public DateTime PublishDate { get; set; } // Needs converter
        [JsonProperty("ownerChannelName")]
        public string OwnerChannelName { get; set; }
        [JsonProperty("uploadDate")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        public DateTime UploadDate { get; set; } // Needs converter
    }
}