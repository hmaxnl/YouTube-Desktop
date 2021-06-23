using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Threading;

using CefNet;

using YouTubeGUI.Cef;
using YouTubeScrap.Core;

namespace YouTubeGUI
{
    public class CefHandler
    {
        private CefApplication _cefApp;
        private CefSettings _cefSettings;
        
        private static Timer _messagePump = null!;
        private const int _messagePumpDelay = 10;
        public CefHandler()
        {
            string cefBinPath;
            bool externalMessagePump = false;
            if (PlatformInfo.IsMacOS)
            {
                externalMessagePump = true;
                cefBinPath = Path.Combine(GetBinPath(), "Contents", "Frameworks", "Chromium Embedded Framework.framework");
            }
            else
                cefBinPath = Path.Combine(GetBinPath(), "Binaries", "cef_bin");
            _cefApp = new CefApplication();
            _cefSettings = new CefSettings()
            {
                MultiThreadedMessageLoop = !externalMessagePump,
                ExternalMessagePump = externalMessagePump,
                WindowlessRenderingEnabled = true,
                LocalesDirPath = Path.Combine(cefBinPath, "Resources", "locales"),
                ResourcesDirPath = Path.Combine(cefBinPath, "Resources"),
                LogSeverity = CefLogSeverity.Warning,
                LogFile = "cef.log",
                IgnoreCertificateErrors = true,
                UncaughtExceptionStackSize = 100,
                NoSandbox = true
            };
            
            App.FrameworkInitialized += App_FrameworkInitialized!;
            App.FrameworkShutdown += App_FrameworkShutdown!;
            _cefApp.ScheduleMessagePumpWorkCallback = OnScheduleMessagePumpWork;
            try
            {
                _cefApp.Initialize(PlatformInfo.IsMacOS ? cefBinPath : Path.Combine(cefBinPath, "Debug"), _cefSettings);
            }
            catch (Exception e)
            {
                Terminal.Terminal.AppendLog($"Exception: {e.Message}", Terminal.Terminal.LogType.Exception, e);
            }
            if (externalMessagePump)
                CefNetApplication.Run();
        }

        private string GetBinPath()
        {
            return Path.GetDirectoryName(typeof(App).Assembly.Location) ?? string.Empty;
        }
        
        private static void App_FrameworkInitialized(object sender, EventArgs e)
        {
            if (CefNetApplication.Instance.UsesExternalMessageLoop)
            {
                _messagePump = new Timer(_ => Dispatcher.UIThread.Post(CefApi.DoMessageLoopWork), null, _messagePumpDelay, _messagePumpDelay);
            }
        }

        private static void App_FrameworkShutdown(object sender, EventArgs e)
        {
            _messagePump?.Dispose();
        }

        private static async void OnScheduleMessagePumpWork(long delayMs)
        {
            await Task.Delay((int)delayMs);
            Dispatcher.UIThread.Post(CefApi.DoMessageLoopWork);
        }
    }
}