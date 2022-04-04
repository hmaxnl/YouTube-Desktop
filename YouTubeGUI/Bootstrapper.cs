using Avalonia.Logging;
using Serilog;
using YouTubeGUI.Stores;
using YouTubeGUI.ViewModels;
using YouTubeGUI.Windows;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Settings;
using LogEventLevel = Serilog.Events.LogEventLevel;

namespace YouTubeGUI
{
    public class Bootstrapper
    {
        // Gets called when windows can be created.
        public readonly NotifyBootstrapInitialized NotifyInitialized;
        public Bootstrapper(string[] mainArgs)
        {
            // Setup SeriLog
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#endif
                .WriteTo.Console()
                .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .WriteTo.File(Defaults.LogLocation, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Bootstrapping...");
            Log.Debug("Test");

            NotifyInitialized += OnNotifyInitialized;

            SettingsManager.LoadSettings();
            UserManager.BuildUser();
            
            //Program.LibVlcManager = new LibVlcManager();
            
            //BUG: CEF does not work in 'debug' mode! It does some weird stuff that i do not know what.
            //CefManager.InitializeCef(mainArgs);
        }
        private void OnNotifyInitialized()
        {
            Program.MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            Program.MainWindow.Show();
        }
    }
    public delegate void NotifyBootstrapInitialized();
}