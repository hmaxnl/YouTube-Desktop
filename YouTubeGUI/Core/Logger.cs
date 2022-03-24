using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using YouTubeScrap.Core;

namespace YouTubeGUI.Core
{
    /* TODO: This needs to be reimplemented in a better way!!! */
    public static class Logger
    {
        public static bool EnableLogging = true;
        public static LogTerminal Terminal = new LogTerminal();
        private static readonly BackgroundWorker BgWorker;
        private static readonly Queue<QueData> BgWorkQue = new Queue<QueData>();
        static Logger()
        {
            BgWorker = new BackgroundWorker();
            BgWorker.DoWork += BgWorkerOnDoWork;
            BgWorker.RunWorkerCompleted += BgWorkerOnRunWorkerCompleted;
        }

        public static void Log(string msg, LogType logType = LogType.Info, StackTrace st = null!, string caller = "", string unmanagedLib = "")
        {
            lock (BgWorker)
            {
                QueData logQue = new QueData()
                {
                    Message = msg,
                    LoggingTye = logType,
                    StackTraceToLog = st,
                    Caller = caller,
                    UnmanagedLib = unmanagedLib
                };
                if (BgWorker.IsBusy)
                    BgWorkQue.Enqueue(logQue);
                else
                    BgWorker.RunWorkerAsync(logQue);
            }
        }
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

        public static FileStream StreamLocation
        {
            get
            {
                return new FileStream(WhereToLog, FileMode.Open, FileAccess.Read);
            }
        }

        
        private static void BgWorkerOnRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            lock (BgWorker)
            {
                if (BgWorkQue.Count <= 0) return;
                if (BgWorker.IsBusy) return;
                BgWorker.RunWorkerAsync(BgWorkQue.Dequeue());
            }
        }

        private static void BgWorkerOnDoWork(object? sender, DoWorkEventArgs e)
        {
            lock (BgWorker)
            {
                if (!EnableLogging) return;
                QueData qData = e.Argument as QueData;
                if (qData == null) return;
                Terminal.AppendLog(qData.Message, qData.LoggingTye, null, qData.StackTraceToLog, qData.Caller, qData.UnmanagedLib);
                if (qData.LoggingTye == LogType.Debug & !DebugManager.IsDebug) return;
                StringBuilder strLogBuilder = new StringBuilder();
                strLogBuilder.Append($"[{DebugManager.GetDateTimeNow}] ");
                strLogBuilder.Append($"[{qData.LoggingTye.ToString().ToUpper()}] > ");
                if (!qData.UnmanagedLib.IsNullEmpty())
                    strLogBuilder.Append($"[{qData.UnmanagedLib}]=");
                if (!qData.Caller.IsNullEmpty())
                    strLogBuilder.Append($"[{qData.Caller}] > ");
                strLogBuilder.Append(qData.Message);
                WriteToFile(strLogBuilder.ToString());
            }
        }
        private static void WriteToFile(string data)
        {
            Directory.CreateDirectory(LoggingDirectory);
            using FileStream logStream = new FileStream(WhereToLog, FileMode.Append, FileAccess.Write);
            using StreamWriter sWriter = new StreamWriter(logStream);
            sWriter.WriteLine(data);
        }
    }

    public class QueData
    {
        public string Message { get; set; }
        public LogType LoggingTye { get; set; } = LogType.Info;
        public StackTrace? StackTraceToLog { get; set; } = null;
        public string Caller { get; set; } = String.Empty;
        public string UnmanagedLib { get; set; } = String.Empty;
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