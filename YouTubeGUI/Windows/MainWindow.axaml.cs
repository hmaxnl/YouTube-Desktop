using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using YouTubeGUI.ContentWindows;
using YouTubeGUI.Core;

namespace YouTubeGUI.Windows
{
    public class MainWindow : Window
    {
        /* Controls */
        private readonly ContentControl? _contentControlMain;
        
        
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
            _contentControlMain = this.Find<ContentControl>("ContentControlMain");
            if (_contentControlMain != null)
                _contentControlMain.Content = new HomeContent();
        }
        
    }
}