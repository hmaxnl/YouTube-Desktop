using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI.Views
{
    public class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ItemsRepeater_OnElementClearing(object? sender, ItemsRepeaterElementClearingEventArgs e)
        {
        }

        private void ItemsRepeater_OnElementPrepared(object? sender, ItemsRepeaterElementPreparedEventArgs e)
        {
        }
    }
}