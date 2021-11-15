using System.Diagnostics;
using YouTubeScrap.Core;

namespace YouTubeGUI.Core
{
    public class DebugTraceListener : TraceListener
    {
        public DebugTraceListener()
        {
            
        }
        public override void Write(string? message)
        {
            if (!message.IsNullEmpty())
                Logger.Log(message, LogType.Trace, new StackTrace());
        }

        public override void WriteLine(string? message)
        {
            if (!message.IsNullEmpty())
                Logger.Log(message, LogType.Trace, new StackTrace());
        }
    }
}