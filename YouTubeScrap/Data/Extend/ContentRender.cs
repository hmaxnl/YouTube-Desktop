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
            set
            {
                _richItem = value;
                SetVariables();
            }
        }
        private RichItemRenderer _richItem;

        [JsonProperty("richSectionRenderer")]
        public RichSectionRenderer RichSection
        {
            get => _richSection;
            set
            {
                _richSection = value;
                SetVariables();
            }
        }
        private RichSectionRenderer _richSection;

        [JsonProperty("continuationItemRenderer")]
        public ContinuationItemRenderer ContinuationItem
        {
            get => _continuationItemRenderer;
            set
            {
                _continuationItemRenderer = value;
                SetVariables();
            }
        }
        private ContinuationItemRenderer _continuationItemRenderer;

        [JsonProperty("chipCloudChipRenderer")]
        public ChipCloudChipRenderer ChipCloudChip
        {
            get => _cloudChipRenderer;
            set
            {
                _cloudChipRenderer = value;
                SetVariables();
            }
        }
        private ChipCloudChipRenderer _cloudChipRenderer;

        private void SetVariables()
        {
            Variables = new ContentVariables(this);
        }
        public ContentVariables Variables { get; set; }
    }

    public class ContentVariables
    {
        public ContentVariables(ContentRender content)
        {
            Overlay = new OverlayVariables(content);
        }
        public delegate void OnListItemSelect();
        public OverlayVariables Overlay { get; set; }
    }

    public class OverlayVariables
    {
        public OverlayVariables(ContentRender content)
        {
            if (content.RichItem == null) return;
            if (content.RichItem.RichItemContent.VideoRenderer != null)
                SetOverlaysTo(content.RichItem.RichItemContent.VideoRenderer.ThumbnailOverlays);
            if (content.RichItem.RichItemContent.RadioRenderer != null)
                SetOverlaysTo(content.RichItem.RichItemContent.RadioRenderer.ThumbnailOverlays);
        }

        public bool ShowNowPlaying { get; set; } = false;
        
        public ThumbnailOverlayTimeStatusRenderer TimeStatusOverlay { get; set; }
        public List<ThumbnailOverlayToggleButtonRenderer> ToggleButtonOverlays { get; set; } =
            new List<ThumbnailOverlayToggleButtonRenderer>();
        public ThumbnailOverlayNowPlayingRenderer NowPlayingOverlay { get; set; }
        public ThumbnailOverlayHoverTextRenderer HoverTextOverlay { get; set; }
        public ThumbnailOverlayBottomPanelRenderer BottomPanelOverlay { get; set; }
        public ThumbnailOverlayEndorsementRenderer EndorsementOverlay { get; set; }
        public ThumbnailOverlayResumePlaybackRenderer ResumePlaybackOverlay { get; set; }

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
            }
        }
    }
}