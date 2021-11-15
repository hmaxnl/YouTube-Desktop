using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI.Themes.Default.Controller
{
    public class ControllerGUI : UserControl
    {
        public ControllerGUI()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public string TextTest => "Test!";
        public int ThemeFontSize = 20;
    }
}