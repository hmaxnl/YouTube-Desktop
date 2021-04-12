using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YouTubeGUI.View;
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
            DebugWindow ??= new YouTubeGUIDebugBase();
            DebugWindow.Title = "YouTubeGUI Debug";
            DebugWindow.Show();
        }
    }
}