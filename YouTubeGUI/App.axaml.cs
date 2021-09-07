using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YouTubeGUI.Terminal;
using YouTubeGUI.ViewModels;
using YouTubeScrap;
using YouTubeScrap.Core;

namespace YouTubeGUI
{
    public class App : Application
    {
        public static event EventHandler FrameworkInitialized;
        public static event EventHandler FrameworkShutdown;

        
        public static YouTubeGuiMainBase? MainWindow;
        public static YouTubeGuiDebugBase? DebugWindow;
        public static YouTubeService? YouTubeService;

        [STAThread]
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
            Terminal.Terminal.Initialize();
            Trace.Listeners.Add(new DebugTraceListener());
            SettingsManager.LoadSettings();
            
            CefManager.InitializeCef(new string[0]); // TODO: Need to pass in the main args!
            SetupDebug();
            YouTubeService = new YouTubeService();
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
            DebugWindow ??= new YouTubeGuiDebugBase();
            DebugWindow.Title = "...Debug...";
            DebugWindow.Show();
            Terminal.Terminal.AppendLog("Debug mode enabled!");
        }

        public void Shutdown()
        {
            SettingsManager.SaveSettings();
            CefManager.ShutdownCef();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.Shutdown();
        }
        
        private void Startup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            FrameworkInitialized?.Invoke(this, EventArgs.Empty);
        }

        private void Exit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            FrameworkShutdown?.Invoke(this, EventArgs.Empty);
        }
    }
}