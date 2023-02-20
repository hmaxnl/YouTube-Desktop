using System;
using System.IO;
using App.Management;
using App.Views;
using Management;
using Serilog;
using YouTubeScrap;

namespace App
{
    public static class Bootstrap
    {
        private static bool _isInit;
        public static YoutubeUser TestUser { get; private set; }

        public static void Init(string [] args)
        {
            if (_isInit) return;

            Manager.Properties.PropertiesPath = "app_properties.json";
            Manager.Properties.ConfigureDefaultProperties();

            SetupLogging(); // After this we can log! Yay!
            Log.Information("Bootstrapping...");
            
            TestUser = new YoutubeUser();

            WindowManager.Register<MainWindow>("Main", true);
            WindowManager.Register<SettingsWindow>("Settings");

            _isInit = true;
        }

        /// <summary>
        /// Call this to shutdown the application!
        /// </summary>
        public static void Shutdown()
        {
            Log.Information("Shutting down...");
            Manager.Properties.Save();
            Log.CloseAndFlush();
        }

        private static void SetupLogging()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#endif
                .WriteTo.Console()
                .WriteTo.File(Manager.Properties["LogPath"]?.ToString() ?? "app_.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        private static void ConfigureDefaultProperties(this PropertyContainer container)
        {
            container.Add("LogPath", Path.Combine(Directory.GetCurrentDirectory(), "logs", "app_.log"));
            container.Add("TempPath", Path.Combine(Path.GetTempPath(), "ytd_temp"));
            container.Add("StoragePath", Path.Combine(Directory.GetCurrentDirectory(), "Storage"));
            container.Add("ImageCachePath", Path.Combine(container.GetString("TempPath"), "image_cache"));
            
            container.Add("Origin", "https://www.youtube.com", false);
            container.Add("UserAgent", GetUserAgent());
        }
        private static string GetUserAgent()
        {
            // We use a firefox user agent because google changed that logins from apps/CEF will not work. Because of 'security reasons'.
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                    return "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:39.0) Gecko/20100101 Firefox/75.0"; // Windows 32-bit on 64-bit CPU - Firefox 75
                case PlatformID.MacOSX:
                    return "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.10; rv:75.0) Gecko/20100101 Firefox/75.0"; // MacOSX 10.10 Intel CPU - Firefox 75
                case PlatformID.Unix:
                case PlatformID.Other:
                default:
                    return "Mozilla/5.0 (X11; Linux ppc64le; rv:75.0) Gecko/20100101 Firefox/75.0"; // Linux powerPC - Firefox 75
            }
        }
    }
}