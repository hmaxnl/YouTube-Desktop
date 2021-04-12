using Newtonsoft.Json;

namespace YouTubeScrap.Data.Media.Data
{
    public class AudioTrack
    {
        /// <summary>
        /// The caption display name.
        /// </summary>
        /// 
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        /// <summary>
        /// Caption id.
        /// </summary>
        /// 
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// If audio is default.
        /// </summary>
        /// 
        [JsonProperty("audioIsDefault")]
        public bool AudioIsDefault { get; set; }
    }
}