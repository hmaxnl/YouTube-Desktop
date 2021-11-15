using System.Diagnostics;
using YouTubeGUI.Core;

namespace YouTubeGUI
{
    public class Bootstrapper
    {
        public readonly NotifyBootstrapInitialized NotifyInitialized;
        public Bootstrapper(ref WindowManager? wm, ref DebugManager? dm)
        {
            NotifyInitialized += OnNotifyInitialized;
            wm = new WindowManager(ref NotifyInitialized);
            dm = new DebugManager(ref NotifyInitialized);
        }

        private void OnNotifyInitialized()
        {
            Trace.WriteLine("Notify Bootstrap!");
        }
    }
    public delegate void NotifyBootstrapInitialized();
}