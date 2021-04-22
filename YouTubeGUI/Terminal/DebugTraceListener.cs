using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace YouTubeGUI.Terminal
{
    public class DebugTraceListener : TraceListener
    {
        public DebugTraceListener()
        {
            
        }
        public override void Write(string? message)
        {
            if (message is { } or "")
                Terminal.AppendLog(message, Terminal.LogType.Trace, null, new StackTrace());
        }

        public override void WriteLine(string? message)
        {
            if (message is { } or "")
                Terminal.AppendLog(message, Terminal.LogType.Trace, null, new StackTrace());
        }
    }
    public struct QueData
    {
        public StackTrace StackTrace;
        public string Message;
    }
}