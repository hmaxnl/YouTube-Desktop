using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class RichSectionContent
    {
        [JsonProperty("richShelfRenderer")]
        public RichShelfRenderer RichShelfRenderer { get; set; }
        [JsonProperty("inlineSurveyRenderer")]
        public InlineSurveyRenderer InlineSurveyRenderer { get; set; }
    }
}