using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YouTubeGUI.Terminal;
using YouTubeGUI.ViewModels;
using YouTubeScrap;

namespace YouTubeGUI
{
    public class App : Application
    {
        public static YouTubeGuiMainBase? MainWindow;
        public static YouTubeGuiDebugBase? DebugWindow;
        public static YouTubeService? YouTubeService;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Terminal.Terminal.Initialize();
            Trace.Listeners.Add(new DebugTraceListener());
            YouTubeService = new YouTubeService();
            SetupDebug();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            MainWindow = new YouTubeGuiMainBase();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.MainWindow = MainWindow;
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
    }
}