using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Data.Extend
{
    public class ContentRender
    {
        [JsonProperty("richItemRenderer")]
        public RichItemRenderer RichItem
        {
            get => _richItem;
            set => _richItem = value;
        }
        private RichItemRenderer _richItem;

        [JsonProperty("richSectionRenderer")]
        public RichSectionRenderer RichSection
        {
            get => _richSection;
            set => _richSection = value;
        }
        private RichSectionRenderer _richSection;

        [JsonProperty("continuationItemRenderer")]
        public ContinuationItemRenderer ContinuationItem
        {
            get => _continuationItemRenderer;
            set => _continuationItemRenderer = value;
        }
        private ContinuationItemRenderer _continuationItemRenderer;

        [JsonProperty("chipCloudChipRenderer")]
        public ChipCloudChipRenderer ChipCloudChip
        {
            get => _cloudChipRenderer;
            set => _cloudChipRenderer = value;
        }
        private ChipCloudChipRenderer _cloudChipRenderer;
    }

    public class OverlayVariables
    {
        public OverlayVariables(List<ThumbnailOverlay> content)
        {
            if (content == null) return;
            SetOverlaysTo(content);
        }

        public bool ShowNowPlaying { get; set; } = false;

        public ThumbnailOverlayTimeStatusRenderer TimeStatusOverlay { get; set; } =
            new ThumbnailOverlayTimeStatusRenderer();
        public List<ThumbnailOverlayToggleButtonRenderer> ToggleButtonOverlays { get; set; } =
            new List<ThumbnailOverlayToggleButtonRenderer>();
        public ThumbnailOverlayNowPlayingRenderer NowPlayingOverlay { get; set; }
        public ThumbnailOverlayHoverTextRenderer HoverTextOverlay { get; set; }
        public ThumbnailOverlayBottomPanelRenderer BottomPanelOverlay { get; set; }
        public ThumbnailOverlayEndorsementRenderer EndorsementOverlay { get; set; }

        public ThumbnailOverlayResumePlaybackRenderer ResumePlaybackOverlay { get; set; } =
            new ThumbnailOverlayResumePlaybackRenderer();

        private void SetOverlaysTo(List<ThumbnailOverlay> overlays)
        {
            if (overlays == null) return;
            foreach (ThumbnailOverlay overlay in overlays)
            {
                if (overlay.TimeStatusRenderer != null)
                    TimeStatusOverlay = overlay.TimeStatusRenderer;
                if (overlay.ToggleButtonRenderer != null)
                    ToggleButtonOverlays.Add(overlay.ToggleButtonRenderer);
                if (overlay.NowPlayingRenderer != null)
                    NowPlayingOverlay = overlay.NowPlayingRenderer;
                if (overlay.HoverTextRenderer != null)
                    HoverTextOverlay = overlay.HoverTextRenderer;
                if (overlay.BottomPanelRenderer != null)
                    BottomPanelOverlay = overlay.BottomPanelRenderer;
                if (overlay.EndorsementRenderer != null)
                    EndorsementOverlay = overlay.EndorsementRenderer;
                if (overlay.ResumePlaybackRenderer != null)
                    ResumePlaybackOverlay = overlay.ResumePlaybackRenderer;
                
                // For TESTING!
                /*Random r = new Random();
                ResumePlaybackOverlay = new ThumbnailOverlayResumePlaybackRenderer() { PercentDurationWatched = r.Next(0, 100) };
                BottomPanelOverlay = new ThumbnailOverlayBottomPanelRenderer() { Icon = "MIX" };
                HoverTextOverlay = new ThumbnailOverlayHoverTextRenderer()
                {
                    Text = new TextLabel() { SimpleText = "Play now!"},
                    Icon = "PLAY_ALL"
                };*/
            }
        }
    }
}