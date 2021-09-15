using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YouTubeGUI.Terminal;
using YouTubeGUI.ViewModels;
using YouTubeScrap;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI
{
    public class App : Application
    {
        public static event EventHandler? FrameworkInitialized;
        public static event EventHandler? FrameworkShutdown;

        // Public objects used in the application.
        public static YouTubeGuiMainBase? MainWindow;
        public static YouTubeGuiDebugBase? DebugWindow;
        public static YouTubeService? YouTubeService;
        public static YoutubeUser CurrentUser = null!;

        private static bool _isDebugInitialized = false;

        [STAThread]
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            // Setup the debug stuff(Called only if it is a debug build!).
            SetupDebug();
            // Setup the terminal and trace listeners.
            Terminal.Terminal.Initialize();
            Trace.Listeners.Add(new DebugTraceListener());
            // Load settings.
            SettingsManager.LoadSettings();
            //BUG: Somehow CEF fires up 2 more debug windows (Only seen this on Linux, not tested it on other platforms) that are transparent.
            //BUG: Idk what causing this but it is some sort of a bug, need to look into that. For now we are not calling the CEF initializer.
            //CefManager.InitializeCef(Array.Empty<string>());
            
            // Setup the user and create the youtube service.
            CurrentUser = new YoutubeUser(YoutubeUser.ReadCookies());
            YouTubeService = new YouTubeService(CurrentUser);
            // Create the main window and open.
            MainWindow = new YouTubeGuiMainBase();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = MainWindow;
                desktop.Startup += Startup;
                desktop.Exit += Exit;
            }
            base.OnFrameworkInitializationCompleted();
        }
        [Conditional("DEBUG")]
        private static void SetupDebug()
        {
            if (!_isDebugInitialized)
            {
                DebugWindow ??= new YouTubeGuiDebugBase();
                DebugWindow.Title = "...Debug...";
                DebugWindow.Show();
                _isDebugInitialized = true;
                Terminal.Terminal.AppendLog("Debug mode enabled!");
            }
        }

        public void Shutdown()
        {
            SettingsManager.SaveSettings();
            CefManager.ShutdownCef();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.Shutdown();
        }
        
        private void Startup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            FrameworkInitialized?.Invoke(this, EventArgs.Empty);
        }

        private void Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            FrameworkShutdown?.Invoke(this, EventArgs.Empty);
        }
    }
}