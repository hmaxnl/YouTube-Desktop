using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI.Controls
{
    public class ThumbnailOverlayView : UserControl
    {
        public ThumbnailOverlayView()
        {
            InitializeComponent();
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
    }
}