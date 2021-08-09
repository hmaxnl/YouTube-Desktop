using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CefNet;

namespace YouTubeGUI
{
    public static class CefManager
    {
        private static CefApplicationImpl app;
        private static Timer messagePump;
        private const int messagePumpDelay = 10;
        public static bool IsInitialized;

        public static void InitializeCef(string[] args)
        {
            // We check here if there is a debugger attached, if true we will NOT initialize the CEF libraries because it will not run under a debugger for some reason.
            if (!IsInitialized && !Debugger.IsAttached)
            {
                string cefPath;
                bool externalMessagePump = args.Contains("--external-message-pump");

                if (PlatformInfo.IsMacOS)
                {
                    externalMessagePump = true;
                    cefPath = Path.Combine(GetProjectPath(), "Contents", "Frameworks", "Chromium Embedded Framework.framework");
                }
                else
                {
                    cefPath = Path.Combine(Directory.GetCurrentDirectory(), "Binaries", "cef_bin");
                }

                var settings = new CefSettings();
                settings.MultiThreadedMessageLoop = !externalMessagePump;
                settings.ExternalMessagePump = externalMessagePump;
                settings.NoSandbox = true;
                settings.WindowlessRenderingEnabled = true;
                settings.LocalesDirPath = Path.Combine(cefPath, "Resources", "locales");
                settings.ResourcesDirPath = Path.Combine(cefPath, "Resources");
                settings.LogSeverity = CefLogSeverity.Warning;
                settings.IgnoreCertificateErrors = true;
                settings.UncaughtExceptionStackSize = 8;

                App.FrameworkInitialized += App_FrameworkInitialized;
                App.FrameworkShutdown += App_FrameworkShutdown;

                app = new CefApplicationImpl();
                app.CefProcessMessageReceived += App_CefProcessMessageReceived;
                app.ScheduleMessagePumpWorkCallback = OnScheduleMessagePumpWork;
                app.Initialize(PlatformInfo.IsMacOS ? cefPath : Path.Combine(cefPath, "Release"), settings);
                IsInitialized = true;
            }
        }

        public static void ShutdownCef()
        {
            if (IsInitialized)
            {
                app.Shutdown();
                app.Dispose();
            }
        }
        private static void App_FrameworkInitialized(object sender, EventArgs e)
        {
            if (CefNetApplication.Instance.UsesExternalMessageLoop)
            {
                messagePump = new Timer(_ => Dispatcher.UIThread.Post(CefApi.DoMessageLoopWork), null, messagePumpDelay, messagePumpDelay);
            }
        }

        private static void App_FrameworkShutdown(object sender, EventArgs e)
        {
            messagePump?.Dispose();
        }

        private static async void OnScheduleMessagePumpWork(long delayMs)
        {
            await Task.Delay((int)delayMs);
            Dispatcher.UIThread.Post(CefApi.DoMessageLoopWork);
        }

        private static string GetProjectPath()
        {
            string projectPath = Path.GetDirectoryName(typeof(App).Assembly.Location);
            string projectName = PlatformInfo.IsMacOS ? "YouTubeGUI.app" : "YouTubeGUI";
            string rootPath = Path.GetPathRoot(projectPath);
            while (Path.GetFileName(projectPath) != projectName)
            {
                if (projectPath == rootPath)
                    throw new DirectoryNotFoundException("Could not find the project directory.");
                projectPath = Path.GetDirectoryName(projectPath);
            }
            return projectPath;
        }

        private static void App_CefProcessMessageReceived(object sender, CefProcessMessageReceivedEventArgs e)
        {
            if (e.Name == "TestV8ValueTypes")
            {
                //TestV8ValueTypes(e.Frame);
                e.Handled = true;
                return;
            }

            if (e.Name == "MessageBox.Show")
            {
                string message = e.Message.ArgumentList.GetString(0);
                //Dispatcher.UIThread.Post(() =>
                //{
                //    var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                //        .GetMessageBoxStandardWindow("title", message);
                //    messageBoxStandardWindow.Show();
                //});
                e.Handled = true;
                return;
            }
        }
    }
}