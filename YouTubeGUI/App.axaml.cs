using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json.Linq;
using YouTubeGUI.Terminal;
using YouTubeGUI.ViewModels;
using YouTubeScrap;

namespace YouTubeGUI
{
    public class App : Application
    {
        public static event EventHandler? FrameworkInitialized;
        public static event EventHandler? FrameworkShutdown;
        
        public static YouTubeGuiMainBase? MainWindow;
        public static YouTubeGuiDebugBase? DebugWindow;
        public static YouTubeService? YouTubeService;
        public static CefHandler? CefHandle;
        
        private static JObject? _initialResponse;
        
        
        [STAThread]
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Terminal.Terminal.Initialize();
            Trace.Listeners.Add(new DebugTraceListener());
            CefHandle = new CefHandler();
            SetupDebug();
            YouTubeService = new YouTubeService(ref _initialResponse);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            MainWindow = new YouTubeGuiMainBase();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = MainWindow;
                desktop.Startup += Startup!;
                desktop.Exit += Exit!;
            }
            base.OnFrameworkInitializationCompleted();
        }

        [Conditional("DEBUG")]
        private void SetupDebug()
        {
            Terminal.Terminal.AppendLog("Debug mode enabled!");
            DebugWindow ??= new YouTubeGuiDebugBase();
            DebugWindow.Title = "YouTubeGUI Debug";
            DebugWindow.Show();
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