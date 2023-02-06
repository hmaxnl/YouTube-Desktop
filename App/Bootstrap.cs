using System.IO;
using App.Management;
using App.Models;
using App.Views;
using Management;
using Serilog;
using YouTubeScrap;

namespace App
{
    public static class Bootstrap
    {
        private static bool _isInit;
        public static void Init(string [] args)
        {
            if (_isInit) return;
            
            Manager.Properties.PropertiesPath = "app_properties.json";
            Manager.Properties.ConfigureDefaultProperties();

            SetupLogging(); // After this we can log! Yay!
            Log.Information("Bootstrapping...");

            Workspace workspace = new Workspace(new YoutubeUser());
            
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
            container.Add("UserAgent", Utilities.GetUserAgent());
        }
    }
}