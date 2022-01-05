using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    /* Saved JSON = yt resp loggedin sectionrenderer.jsonc*/
    [JsonConverter(typeof(JsonPathConverter))]
    public class InlineSurveyRenderer : ITrackingParams
    {
        // dismissalEndpoint
        [JsonProperty("title")]
        public TextElement Title { get; set; }
        [JsonProperty("subtitle")]
        public TextElement Subtitle { get; set; }
        [JsonProperty("inlineContent.compactVideoRenderer")]
        public RichVideoContent InlineContent { get; set; }
        // response/expandableSurveyResponseRenderer feedback implementation.
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("dismissalText")]
        public TextElement DismissalText { get; set; }
        // impressionEndpoints (List of impressionEndpoints)
    }
}