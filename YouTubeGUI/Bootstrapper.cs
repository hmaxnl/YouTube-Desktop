using System.IO;
using Management;
using Serilog;
using YouTubeGUI.Managers;
using YouTubeGUI.ViewModels;
using YouTubeGUI.Windows;
using LogEventLevel = Serilog.Events.LogEventLevel;

namespace YouTubeGUI
{
    public class Bootstrapper
    {
        // Gets called when windows can be created.
        public readonly NotifyBootstrapInitialized NotifyInitialized;
        public Bootstrapper(string[] mainArgs)
        {
            BootstrapperHelper.SetupProps();
            // Setup SeriLog
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#endif
                .WriteTo.Console()
                .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .WriteTo.File(Path.Combine("logs", "youtubed_.log") ?? string.Empty, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Bootstrapping...");

            NotifyInitialized += OnNotifyInitialized;
            
            UserManager.BuildUser();
            
            /* Initializing VLC library. */
            //Program.LibVlcManager = new LibVlcManager();
            
            /* Initializing CEF framework. */
            //BUG: CEF does not work in 'debug' mode! It does some weird stuff that i do not know what.
            //CefManager.InitializeCef(mainArgs);
        }
        private void OnNotifyInitialized()
        {
            Program.MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(WorkspaceManager.DefaultWorkspace)
            };
            Program.MainWindow.Show();
        }
    }

    public static class BootstrapperHelper
    {
        public static void SetupProps()
        {
            Manager.Properties.PropertiesPath = "app_properties.json";
            Manager.Properties.ConfigureDefaultProperties();
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

    public delegate void NotifyBootstrapInitialized();
}