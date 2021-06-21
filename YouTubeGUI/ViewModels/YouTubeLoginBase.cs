using Avalonia.Controls;
using CefNet.Avalonia;
using YouTubeGUI.View;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeLoginBase : YouTubeLoginWindow
    {
        public YouTubeLoginBase()
        {
            WebView loginWebView = this.FindControl<WebView>("CefWebViewLogin");
        }
    }
}