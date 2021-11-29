using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Controls
{
    public class ThumbnailOverlayView : UserControl
    {
        public ThumbnailOverlayView()
        {
            InitializeComponent();
            //DataContext = this;
            OverlaysProperty.Changed
                .Where(args => args.IsEffectiveValueChange)
                .Subscribe(args => SetOverlaysTo((ThumbnailOverlayView)args.Sender, args.NewValue.Value));
            ItemContentProperty.Changed
                .Where(args => args.IsEffectiveValueChange)
                .Subscribe(args => SetContent((ThumbnailOverlayView)args.Sender, args.NewValue.Value));
        }
        
        public ContentRender ItemContent { get; set; }
        
        public ThumbnailOverlayTimeStatusRenderer TimeStatusOverlay { get; set; }

        public List<ThumbnailOverlayToggleButtonRenderer> ToggleButtonOverlays { get; set; } =
            new List<ThumbnailOverlayToggleButtonRenderer>();
        public ThumbnailOverlayNowPlayingRenderer NowPlayingOverlay { get; set; }
        public ThumbnailOverlayHoverTextRenderer HoverTextOverlay { get; set; }
        public ThumbnailOverlayBottomPanelRenderer BottomPanelOverlay { get; set; }
        public ThumbnailOverlayEndorsementRenderer EndorsementOverlay { get; set; }
        public ThumbnailOverlayResumePlaybackRenderer ResumePlaybackOverlay { get; set; }

        private void SetOverlaysTo(ThumbnailOverlayView sender, List<ThumbnailOverlay> overlays)
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
            //DataContext = this;
        }

        private void SetContent(ThumbnailOverlayView sender, ContentRender content)
        {
            ItemContent = content;
            //DataContext = this;
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public static readonly StyledProperty<bool> OnIsVisibleProperty =
            AvaloniaProperty.Register<ThumbnailOverlayView, bool>(nameof(OnIsVisible));
        public bool OnIsVisible
        {
            get => GetValue(OnIsVisibleProperty);
            set => SetValue(OnIsVisibleProperty, value);
        }

        public static readonly AttachedProperty<List<ThumbnailOverlay>> OverlaysProperty =
            AvaloniaProperty.RegisterAttached<ThumbnailOverlayView, List<ThumbnailOverlay>>("Overlays", typeof(ThumbnailOverlayView));
        public static List<ThumbnailOverlay> GetOverlays(ThumbnailOverlayView element)
        {
            return element.GetValue(OverlaysProperty);
        }
        public static void SetOverlays(ThumbnailOverlayView element, List<ThumbnailOverlay> value)
        {
            element.SetValue(OverlaysProperty, value);
        }

        public static readonly AttachedProperty<ContentRender> ItemContentProperty =
            AvaloniaProperty.RegisterAttached<ThumbnailOverlayView, ContentRender>("ItemContent",
                typeof(ThumbnailOverlayView));

        public static ContentRender GetItemContent(ThumbnailOverlayView element)
        {
            return element.GetValue(ItemContentProperty);
        }

        public static void SetItemContent(ThumbnailOverlayView element, ContentRender value)
        {
            element.SetValue(ItemContentProperty, value);
        }
    }
}