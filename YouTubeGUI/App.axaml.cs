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
        public static YouTubeGUIDebugBase? DebugWindow;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Terminal.Terminal.Initialize();
            Trace.Listeners.Add(new DebugTraceListener());
            SetupDebug();
            LibraryHandler.Initialize();
            Terminal.Terminal.AppendLog("Initialized application!");
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
            DebugWindow ??= new YouTubeGUIDebugBase();
            DebugWindow.Title = "YouTubeGUI Debug";
            DebugWindow.Show();
        }
    }
}