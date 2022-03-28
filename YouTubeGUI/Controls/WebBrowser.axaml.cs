using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CefNet.Avalonia;
using Serilog;
using YouTubeGUI.Core;

namespace YouTubeGUI.Controls
{
    public class WebBrowser : UserControl
    {
        public readonly WebView? WebViewBrowser;
        
        public WebBrowser()
        {
            InitializeComponent();
            Grid viewGrid = this.FindControl<Grid>("ViewGrid");
            if (viewGrid != null)
            {
                if (CefManager.IsInitialized)
                {
                    WebViewBrowser = new WebView();
                    viewGrid.Children.Add(WebViewBrowser);
                }
                else
                {
                    Label labelCefDisabled = new Label(){ Content = "CEF is not loaded or disabled!" };
                    viewGrid.Children.Add(labelCefDisabled);
                }
            }
            else
                Log.Information("Could not find grid!");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}