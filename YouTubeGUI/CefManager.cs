using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CefNet;
using CefNet.Net;
using Serilog;
using YouTubeScrap.Core;

namespace YouTubeGUI
{
    public static class CefManager
    {
        public static CefApplicationImpl CefApp => app;
        public static CefCookieManager CookieManager => _cookieManager;
        public static bool IsInitialized;
        public static string CachePath { get => Path.Combine(Directory.GetCurrentDirectory(), "Cache", "CEF"); }

        private static CefCookieManager _cookieManager;
        private static CefApplicationImpl app;
        private static Timer messagePump;
        private const int messagePumpDelay = 10;

        public static void InitializeCef(string[] args)
        {
            // We check here if there is a debugger attached, if true we will NOT initialize the CEF libraries because it will not run under a debugger for some reason.
            if (!IsInitialized && !Debugger.IsAttached)
            {
                try
                {
                    string cefPath;
                    bool externalMessagePump = args.Contains("--external-message-pump");

                    if (PlatformInfo.IsMacOS)
                    {
                        externalMessagePump = true;
                        cefPath = Path.Combine(GetProjectPath(), "Contents", "Frameworks", "Chromium Embedded Framework.framework");
                    }
                    else
                        cefPath = Path.Combine(Directory.GetCurrentDirectory(), "Binaries", "cef_bin");

                    var settings = new CefSettings()
                    {
                        MultiThreadedMessageLoop = !externalMessagePump,
                        ExternalMessagePump = externalMessagePump,
                        NoSandbox = true,
                        WindowlessRenderingEnabled = true,
                        LocalesDirPath = Path.Combine(cefPath, "Resources", "locales"),
                        ResourcesDirPath = Path.Combine(cefPath, "Resources"),
                        LogSeverity = CefLogSeverity.Error,
                        UncaughtExceptionStackSize = 8,
                        LogFile = "cef_debug.log",
                        CachePath = CachePath,
                        UserDataPath = Path.Combine(CachePath, ".UserData"),
                        PersistSessionCookies = true,
                        UserAgent = SettingsManager.Settings.UserAgent
                    };

                    App.FrameworkInitialized += App_FrameworkInitialized;
                    App.FrameworkShutdown += App_FrameworkShutdown;

                    app = new CefApplicationImpl();
                    app.CefProcessMessageReceived += App_CefProcessMessageReceived;
                    app.ScheduleMessagePumpWorkCallback = OnScheduleMessagePumpWork;
                    app.Initialize(PlatformInfo.IsMacOS ? cefPath : Path.Combine(cefPath, "Release"), settings);
                    IsInitialized = true;
                }
                catch (Exception e)
                {
                    Log.Warning(e, "Cold not initialize the CEF framework!");
                    return;
                }
                _cookieManager = CefRequestContext.GetGlobalContext().GetCookieManager(null);
            }
        }

        /// <summary>
        /// Get the cookies from cef if the user in logged in to youtube/google
        /// </summary>
        /// <returns>CookieCollection</returns>
        public static CookieCollection GetCookies()
        {
            CookieCollection cookieCollection = new CookieCollection();
            Log.Information("Getting cookies...");
            List<CefNetCookie> cookies = _cookieManager.GetCookiesAsync(CancellationToken.None).Result.Cast<CefNetCookie>().ToList();
            foreach (CefNetCookie cefCookie in cookies)
            {
                Cookie cookie = new Cookie()
                {
                    Domain = cefCookie.Domain,
                    Expired = cefCookie.Expired,
                    Expires = cefCookie.Expires ?? DateTime.MaxValue,
                    Name = cefCookie.Name,
                    Value = cefCookie.Value,
                    HttpOnly = cefCookie.HttpOnly,
                    Path = cefCookie.Path,
                    Secure = cefCookie.Secure
                };
                cookieCollection.Add(cookie);
            }
            return cookieCollection;
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