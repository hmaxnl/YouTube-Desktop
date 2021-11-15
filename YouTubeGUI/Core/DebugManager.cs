using System;
using System.Diagnostics;
using YouTubeGUI.Windows;

namespace YouTubeGUI.Core
{
    public class DebugManager
    {
        public static bool IsDebug
        {
            get
            {
#if(DEBUG)
                return true;
#else
                return false;
#endif
            }
        }

        public static string GetDateTimeNow => DateTime.Now.ToString("T");
        public DebugManager(ref NotifyBootstrapInitialized notify)
        {
            notify += Notify;
            Trace.Listeners.Add(new DebugTraceListener());
        }
        private static DebugWindow? _window;
        
        private void Notify()
        {
            _window = new DebugWindow();
            _window.Title = "...Debug...";
            _window.Show();
        }
    }
}