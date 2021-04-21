using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YouTubeGUI.ViewModels;

namespace YouTubeGUI
{
    public class App : Application
    {
        public YouTubeGuiMainBase? MainWindow;
        public YouTubeGUIDebugBase? DebugWindow;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Terminal.Terminal.Initialize();
            SetupDebug();
            Terminal.Terminal.AppendLog("Initializing application!");
            Terminal.Terminal.AppendLog("Test", Terminal.Terminal.LogType.Warning);
            Terminal.Terminal.AppendLog("Test", Terminal.Terminal.LogType.Error);
            Terminal.Terminal.AppendLog("Test", Terminal.Terminal.LogType.Exception, new NullReferenceException("Test message", new Exception("Inner exception test")));
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