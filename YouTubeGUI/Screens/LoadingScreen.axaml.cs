using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI.Screens
{
    public class LoadingScreen : UserControl
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}