using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using App.Views;
using Management;
using ReactiveUI;
using Splat;
using YouTubeScrap.Core.Settings;

namespace App
{
    public partial class App : Application
    {
        public App()
        {
            // Splat assembly scanning.
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        public override void Initialize()
        {
            Registry.Merge(Defaults.DefaultVariables);
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}