using System;
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
        
        //public LogTerminal LogTerminal { get; }

        public static string GetDateTimeNow => DateTime.Now.ToString("T");
        public DebugManager(ref NotifyBootstrapInitialized notify)
        {
            //LogTerminal = new LogTerminal();
            //notify += Notify;
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