using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvalonBrowser
{
    public class BrowserWindow : Window
    {
        public BrowserWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}