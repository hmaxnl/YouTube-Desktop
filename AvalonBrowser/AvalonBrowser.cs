using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Avalonia;
using Xilium.CefGlue;

namespace AvalonBrowser
{
    public static class AvalonBrowser
    {
        private static bool isAvaloniaInitialized;
        private static CefSettings cefSettings;
        private static CefApplication cefApp;
        private static CefMainArgs cefArgs;
        private static readonly string cefTempPath = Path.Combine(Directory.GetCurrentDirectory(), "cef_temp");
        private static readonly string cefBinPath = Path.Combine(Directory.GetCurrentDirectory(), "Binaries", "cef_bin");

        private static Thread subThreadUI;
        private static string[] argrs;
        
        public static void Initialize(string[] argruments)
        {
            if (!isAvaloniaInitialized)
            {
                argrs = argruments;
                subThreadUI = new Thread(new ThreadStart(StartAppBuilder));
                subThreadUI.Start();
                isAvaloniaInitialized = true;
            }

            cefApp = new CefApplication();
            
            cefSettings = new CefSettings()
            {
                LogFile = "cef.log",
                CachePath = Path.Combine(cefTempPath, "cache"),
                ResourcesDirPath = Path.Combine(cefBinPath, "Resources"),
                LocalesDirPath = Path.Combine(cefBinPath, "Locales")
            };
            
            cefArgs = new CefMainArgs(argruments);
            
            CefRuntime.Initialize(cefArgs, cefSettings, cefApp, IntPtr.Zero);
            CefRuntime.Load();
        }


        private static void StartAppBuilder()
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(argrs);
        }
        private static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }
    }
}