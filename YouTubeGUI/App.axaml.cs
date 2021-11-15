using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI
{
    public class App : Application
    {
        [STAThread]
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Program.Instance?.NotifyInitialized();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Program.MainWindow;
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}