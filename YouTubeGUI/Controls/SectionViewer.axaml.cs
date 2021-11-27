using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI.Controls
{
    public class SectionViewer : UserControl
    {
        public SectionViewer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}