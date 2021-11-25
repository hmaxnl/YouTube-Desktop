using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Controls
{
    public class ThumbnailOverlayView : UserControl
    {
        /*[Content]
        public Dictionary<Type, IControl> Controls { get; } = new Dictionary<Type, IControl>();*/
        public ThumbnailOverlayView()
        {
            InitializeComponent();
            OverlaysProperty.Changed
                .Where(args => args.IsEffectiveValueChange)
                .Subscribe(args => SetOverlaysTo((ThumbnailOverlayView)args.Sender, args.NewValue.Value));
        }

        public ThumbnailOverlayTimeStatusRenderer TimeStatusOverlay { get; set; }
        public List<ThumbnailOverlayToggleButtonRenderer> ToggleButtonOverlay { get; set; }

        private void SetOverlaysTo(ThumbnailOverlayView sender, List<ThumbnailOverlay> overlays)
        {
            if (overlays == null) return;
            foreach (ThumbnailOverlay overlay in overlays)
            {
                if (overlay.TimeStatusRenderer != null)
                    TimeStatusOverlay = overlay.TimeStatusRenderer;
                sender.DataContext = this;
            }
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
    }
}