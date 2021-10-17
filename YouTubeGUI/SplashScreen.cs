using System.ComponentModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using YouTubeGUI.View;

namespace YouTubeGUI
{
    public class Splash : YouTubeSplash
    {
        public string StatusInit => _status;

        private string _status = "YouTube Desktop v0.1";
        public Splash()
        {
            Trace.WriteLine("Setup splash properties...");
            DataContext = this;
            /*WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = -1;*/
        }
        public void ShowSplash()
        {
            Trace.WriteLine("Opening Splash...");
            Show();
        }
        public void CloseSplash()
        {
            Trace.WriteLine("Closing Splash...");
            Hide();
        }
    }
}