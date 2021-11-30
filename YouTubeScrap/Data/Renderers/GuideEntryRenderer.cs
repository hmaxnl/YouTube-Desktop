using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Renderers
{
    public class GuideEntryRenderer : ITrackingParams
    {
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("badges")]
        public GuideBadge Badges { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("formattedTitle")]
        public TextLabel FormattedTitle { get; set; }
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
        [JsonProperty("entryData")]
        public GuideEntryData EntryData { get; set; }
        [JsonProperty("serviceEndpoint")]
        public ServiceEndpoint ServiceEndpoint { get; set; }
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
        [JsonProperty("isPrimary")]
        public bool IsPrimary { get; set; }
        [JsonProperty("presentationStyle")]
        public string PresentationStyle { get; set; }
    }
}