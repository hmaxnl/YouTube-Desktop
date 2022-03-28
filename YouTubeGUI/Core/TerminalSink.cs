using System;
using Serilog.Core;
using Serilog.Events;

namespace YouTubeGUI.Core
{
    public class TerminalSink : ILogEventSink
    {
        private IFormatProvider _formatProvider;
        public TerminalSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            
        }
    }
}