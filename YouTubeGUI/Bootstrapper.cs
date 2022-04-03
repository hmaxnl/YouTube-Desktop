using System;
using Serilog;
using Serilog.Events;
using YouTubeGUI.Core;
using YouTubeGUI.Stores;
using YouTubeGUI.ViewModels;
using YouTubeGUI.Windows;
using YouTubeScrap.Core;

namespace YouTubeGUI
{
    public class Bootstrapper
    {
        // Gets called when windows can be created.
        public readonly NotifyBootstrapInitialized NotifyInitialized;
        public Bootstrapper(ref DebugManager? dm, string[] mainArgs)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .CreateLogger();
            dm = new DebugManager(ref NotifyInitialized);
            Log.Information("Bootstrapping...");
            Log.Error(new Exception("Test exception!"),"Test error!");
            Log.Fatal("Test fatal!");
            Log.Warning("Test warning!");
            Log.Debug("Test debug!");
            Log.Verbose("Test verbose!");
            NotifyInitialized += OnNotifyInitialized;
            SettingsManager.LoadSettings();
            UserManager.BuildUser();
            //Program.LibVlcManager = new LibVlcManager();
            //BUG: Somehow CEF fires up 2 more debug windows (Only seen this on Linux, not tested it on other platforms) that are transparent.
            //BUG: Idk what causing this but it is some sort of a bug, need to look into that. For now we are not calling the CEF initializer.
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