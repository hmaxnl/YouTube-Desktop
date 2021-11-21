using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI.Screens
{
    public class HomeScreen : UserControl
    {
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}