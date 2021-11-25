using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI.Controls
{
    public class HomeScreenContentList : UserControl
    {
        public HomeScreenContentList()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}