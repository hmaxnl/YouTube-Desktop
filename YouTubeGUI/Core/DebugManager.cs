using System.Diagnostics;

namespace YouTubeGUI.Core
{
    public class DebugManager
    {
        public DebugManager(ref NotifyBootstrapInitialized notify)
        {
            notify += Notify;
        }

        private void Notify()
        {
            Trace.WriteLine("Notify DebugManager!");
        }
    }
}