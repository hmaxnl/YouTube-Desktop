using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using YouTubeScrap.Core;

namespace YouTubeGUI.Core
{
    /*Will be further implemented with some caching and of loading to threads, for now it works.*/
    public static class Logger
    {
        public static bool EnableLogging = true;
        public static LogTerminal Terminal = new LogTerminal();
        public static string LoggingDirectory
        {
            get
            {
                if (_loggingDirectory.IsNullEmpty())
                    _loggingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs", DateTime.Now.ToString("yyyy"));
                return _loggingDirectory;
            }
            set
            {
                if (value != String.Empty)
                    _loggingDirectory = value;
            }
        }

        private static string _loggingDirectory = string.Empty;
        public static string WhereToLog
        {
            get
            {
                if (_whereToLog.IsNullEmpty())
                    _whereToLog = Path.Combine(LoggingDirectory, $"Log-{DateTime.Now:dd-MMM}.log");
                return _whereToLog;
            }
            set
            {
                if (value != String.Empty)
                    _whereToLog = value;
            }
        }
        private static string _whereToLog = string.Empty;
        

        public static void Log(string msg, LogType logType = LogType.Info, StackTrace st = null!)
        {
            LogExtend(msg, logType, null, st);
        }

        public static void LogExtend(string msg, LogType logType = LogType.Info, string caller = "", StackTrace st = null!)
        {
            if (!EnableLogging) return;
            Terminal.AppendLog(msg, logType, null, st, caller);
            if (logType == LogType.Debug & !DebugManager.IsDebug) return;
            StringBuilder strLogBuilder = new StringBuilder();
            strLogBuilder.Append($"[{DebugManager.GetDateTimeNow}] ");
            if (caller != string.Empty)
                strLogBuilder.Append($"[{caller}] > ");
            strLogBuilder.Append($"[{logType.ToString().ToUpper()}] > ");
            strLogBuilder.Append(msg);
            WriteToFile(strLogBuilder.ToString());
        }
        private static void WriteToFile(string data)
        {
            Directory.CreateDirectory(LoggingDirectory);
            using FileStream logStream = new FileStream(WhereToLog, FileMode.Append, FileAccess.Write);
            using StreamWriter sWriter = new StreamWriter(logStream);
            sWriter.WriteLine(data);
        }
    }

    public enum LogType
    {
        Info,
        Debug,
        Trace,
        Warning,
        Error,
        Exception,
        Fatal
    }
}