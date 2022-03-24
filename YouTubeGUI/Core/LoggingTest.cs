using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace YouTubeGUI.Core
{
    public static class LoggingTest
    {
        public static bool TraceLogging { get; set; } = false;
        
        public static void Log(string msg, int skipFrames = 1) => LogDataFunc(msg, LogLevel.Log, new StackFrame(skipFrames));
        public static void Debug(string msg) => LogDataFunc(msg, LogLevel.Debug, new StackFrame(1));
        public static void Warning(string msg) => LogDataFunc(msg, LogLevel.Warning, new StackFrame(1));
        public static void Error(Exception ex) => LogDataFunc(logLevel: LogLevel.Error,stackFrame: new StackFrame(1), ex: ex);
        public static void Fatal(string msg, Exception? ex = null) => LogDataFunc(msg, LogLevel.Fatal, new StackFrame(1), ex: ex);

        private static string LogLocation => Path.Combine(Directory.GetCurrentDirectory(), "logs_test");

        private static void LogDataFunc(string msg = "", LogLevel logLevel = LogLevel.Log, StackFrame? stackFrame = null, Exception? ex = null)
        {
            MethodBase? method = stackFrame?.GetMethod();
            LogData lData = new LogData()
            {
                Message = msg,
                FunctionName = method?.Name ?? "No Function!",
                ClassName = method?.DeclaringType?.Name ?? "No class!",
                Namespace = method?.DeclaringType?.Namespace ?? "No namespace!",
                Assembly = method?.DeclaringType?.Assembly.ManifestModule.Name ?? "No assembly!",
                LogLevel = logLevel,
                LogTime = DateTime.Now,
                Exception = ex
            };
            if (!Directory.Exists(LogLocation))
                Directory.CreateDirectory(LogLocation);
            FileStream fStream = new FileStream(Path.Combine(LogLocation, $"{lData.LogLevel.ToString().ToUpper()}_{lData.LogTime.Ticks}.json"), FileMode.Create);
            try
            {
                var test = JsonConvert.SerializeObject(lData);
                using StreamWriter writer = new StreamWriter(fStream);
                writer.Write(test);
                writer.Flush();
            }
            finally
            {
                fStream.Close();
            }
        }
    }
    struct LogData
    {
        public string Message;
        public string FunctionName;
        public string ClassName;
        public string Namespace;
        public string Assembly;
        public LogLevel LogLevel;
        public DateTime LogTime;
        public Exception? Exception;
    }

    public enum LogLevel
    {
        Log,
        Debug,
        Warning,
        Error,
        Fatal
    }
}