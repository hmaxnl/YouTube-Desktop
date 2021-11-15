using System.Diagnostics;
using Avalonia.Controls;

namespace YouTubeGUI.Core
{
    public class WindowManager
    {
        public WindowManager(ref NotifyBootstrapInitialized notify)
        {
            Trace.WriteLine("Window manager initializing!");
            notify += Notify;
        }

        private void Notify()
        {
            Trace.WriteLine("Notify WindowManager!");
            // when avalonia is setup, then we can open windows.
        }
    }
}