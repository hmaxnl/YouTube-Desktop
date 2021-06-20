using System;
using System.Diagnostics;
using System.IO;
using Avalonia;
using CefNet;
using YouTubeGUI.Cef;
using YouTubeScrap.Core;

namespace YouTubeGUI
{
    public class CefHandler
    {
        private CefApplication _cefApp;
        private CefSettings _cefSettings;
        public CefHandler()
        {
            string cefBinPath = Path.Combine(GetBinPath(), "Binaries", "cef_bin");
            _cefApp = new CefApplication();
            _cefSettings = new CefSettings()
            {
                MultiThreadedMessageLoop = true,
                NoSandbox = true,
                WindowlessRenderingEnabled = true,
                LocalesDirPath = Path.Combine(cefBinPath, "Resources", "locales"),
                ResourcesDirPath = Path.Combine(cefBinPath, "Resources"),
                LogSeverity = CefLogSeverity.Warning,
                LogFile = "cef.log",
                IgnoreCertificateErrors = true,
                UncaughtExceptionStackSize = 10
            };
            _cefApp.Initialize(Path.Combine(cefBinPath, "Release"), _cefSettings);
        }

        private string GetBinPath()
        {
            return Path.GetDirectoryName(typeof(App).Assembly.Location) ?? string.Empty;
        }
    }
}