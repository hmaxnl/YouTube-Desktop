using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using Avalonia.Controls;
using CefNet;
using YouTubeGUI.Controls;
using YouTubeGUI.View;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeLoginBase : YouTubeLoginWindow
    {
        public readonly WebBrowser? Browser;
        public YouTubeLoginBase()
        {
            if (CefManager.IsInitialized)
            {
                Browser = this.FindControl<WebBrowser>("WebBrowserControl");
                if (Browser != null)
                {
                    Trace.WriteLine("Found the browser control!");
                    Browser.WebViewBrowser.InitialUrl = "about:blank";
                }
                else
                    Trace.WriteLine("Could not find the browser control!");
            }
            else
                Terminal.Terminal.AppendLog("Cef is not initialized!", Terminal.Terminal.LogType.Error);
            
            Browser.WebViewBrowser.Navigated += WebViewBrowserOnNavigated;
        }

        private void WebViewBrowserOnNavigated(object? sender, NavigatedEventArgs e)
        {
            if (e.Url == "https://www.youtube.com/")
            {
                //CefManager.GetCookies();
                Trace.WriteLine($"Landed on page: {e.Url}");
            }
        }

        public void PushLogin(string loginUrl = "")
        {
            if (Browser != null)
            {
                if (!IsVisible)
                {
                    Trace.WriteLine("Showing window!");
                    Show();
                }

                if (loginUrl != String.Empty)
                {
                    Trace.WriteLine($"Navigating to: {loginUrl}");
                    Browser?.WebViewBrowser?.Navigate(loginUrl);
                }
            }
            else
                Trace.WriteLine("Browser object is NULL!");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Trace.WriteLine("Hiding window!");
            Hide();
        }
    }
}