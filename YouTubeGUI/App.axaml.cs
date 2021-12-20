using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace YouTubeGUI
{
    public class App : Application
    {
        public static event EventHandler? FrameworkInitialized;
        public static event EventHandler? FrameworkShutdown;
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
                desktop.Startup += Startup;
                desktop.Exit += Exit;
            }
            base.OnFrameworkInitializationCompleted();
        }
        // For CEF
        private void Startup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            FrameworkInitialized?.Invoke(this, EventArgs.Empty);
        }
        // For CEF
        private void Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            FrameworkShutdown?.Invoke(this, EventArgs.Empty);
        }
    }
}