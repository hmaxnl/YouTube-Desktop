using System;
using System.ComponentModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CefNet;
using YouTubeGUI.Controls;
using YouTubeGUI.Core;

namespace YouTubeGUI.Windows
{
    public class LoginWindow : Window
    {
        public readonly WebBrowser? Browser;
        public LoginWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            if (CefManager.IsInitialized)
            {
                Browser = this.FindControl<WebBrowser>("WebBrowserControl");
                if (Browser != null)
                {
                    Trace.WriteLine("Found the browser control!");
                    if (Browser.WebViewBrowser != null)
                    {
                        Browser.WebViewBrowser.InitialUrl = "about:blank";
                        Browser.WebViewBrowser.Navigated += WebViewBrowserOnNavigated;
                    }
                }
                else
                    Trace.WriteLine("Could not find the browser control!");
            }
            else
                Logger.Log("CEF is not initailized!", LogType.Warning);
        }
        
        private void WebViewBrowserOnNavigated(object? sender, NavigatedEventArgs e)
        {
            if (e.Url == "https://www.youtube.com/")
            {
                //CefManager.GetCookies();
                Trace.WriteLine($"Landed on page: {e.Url}");
            }
        }
        public void NavigateTo(string navigateUrl = "")
        {
            if (Browser != null)
            {
                if (!IsVisible)
                {
                    Trace.WriteLine("Showing window!");
                    Show();
                }

                if (navigateUrl != String.Empty)
                {
                    Trace.WriteLine($"Navigating to: {navigateUrl}");
                    Browser?.WebViewBrowser?.Navigate(navigateUrl);
                }
            }
            else
                Trace.WriteLine("Browser object is NULL!");
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            NavigateTo("about:blank");
            Hide();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}