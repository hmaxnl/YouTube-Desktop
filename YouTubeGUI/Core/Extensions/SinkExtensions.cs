using System;
using Serilog;
using Serilog.Configuration;

namespace YouTubeGUI.Core.Extensions
{
    public static class SinkExtensions
    {
        public static LoggerConfiguration Terminal(this LoggerSinkConfiguration loggerConfiguration,
            IFormatProvider formatProvider = null) => loggerConfiguration.Sink(new TerminalSink(formatProvider));
    }
}