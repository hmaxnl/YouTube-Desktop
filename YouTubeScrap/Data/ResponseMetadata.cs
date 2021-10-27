using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YouTubeScrap.Data
{
    public class ResponseMetadata
    {
        [JsonProperty("responseContext")]
        public ResponseContext RespContext { get; set; }
        [JsonProperty("contents")]
        public Contents Contents { get; set; }
    }
}