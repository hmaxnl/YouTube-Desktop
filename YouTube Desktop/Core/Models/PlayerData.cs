using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models
{
    public class PlayerData
    {
        private string _jsUrl;
        [JsonProperty("rootElementId")]
        public string RootElement { get; set; }
        [JsonProperty("jsUrl")]
        public string JSUrl { get => _jsUrl; set { _jsUrl = string.Format("https://www.youtube.com{0}", value); } }
        [JsonProperty("cssUrl")]
        public string CSSUrl { get; set; }
        [JsonProperty("contextId")]
        public string ContextId { get; set; }
        [JsonProperty("eventLabel")]
        public string EventLabel { get; set; }
        [JsonProperty("contentRegion")]
        public string ContentRegion { get; set; }
        [JsonProperty("hl")]
        public string HL { get; set; }
        [JsonProperty("hostLanguage")]
        public string HostLanguage { get; set; }
        [JsonProperty("innertubeApiKey")]
        public string InnertubeAPIKey { get; set; }
        [JsonProperty("innertubeApiVersion")]
        public string InnertubeAPIVersion { get; set; }
        [JsonProperty("innertubeContextClientVersion")]
        public string InnertubeContextClientVersion { get; set; }

        public bool Success { get; set; }
        public Exception ExceptionError { get; set; }
    }
}