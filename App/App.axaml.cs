using System.Reflection;
using App.Management;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Serilog;
using Splat;

namespace App
{
    public class App : Application
    {
        public App()
        {
            // Splat assembly scanning.
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = WindowManager.GetMainWindow;
                desktop.ShutdownRequested += DesktopOnShutdownRequested;
            }
            base.OnFrameworkInitializationCompleted();
        }

        private static void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            Log.Information("Received shutdown request from: {0}", sender);
            Bootstrap.Shutdown();
        }
    }
}