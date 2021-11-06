using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class RadioRenderer : ITrackingParams
    {
        [JsonProperty("playlistId")]
        public string PlaylistId { get; set; }
        [JsonProperty("title")]
        public TextLabel Title { get; set; }
        [JsonProperty("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
        [JsonProperty("videoCountText.runs")]
        public List<TextRun> VideoCountText { get; set; }
        // navigationEndpoint | First figure out the endpoint implementation!!!
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        // For now we use the 'VideoRenderer' class. If the received JSON changes too much, it will be placed in its own class. This is just some class recycling!
        [JsonProperty("videos.childVideoRenderer")]
        public List<VideoRenderer> Videos { get; set; }
        [JsonProperty("thumbnailText")]
        public TextLabel ThumbnailText { get; set; }
        [JsonProperty("longByLineText")]
        public TextLabel LongByLineText { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("thumbnailOverlays")]
        public List<ThumbnailOverlay> ThumbnailOverlays { get; set; }
        [JsonProperty("videoCountShortText")]
        public List<TextRun> VideoCountShortText { get; set; }
    }
}