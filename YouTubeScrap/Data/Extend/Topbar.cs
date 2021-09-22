using Newtonsoft.Json;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Extend
{
    public class Topbar : ITrackingParams
    {
        [JsonProperty("logo")]
        public TopbarLogo Logo { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        public string TrackingParams { get; set; }
    }
}