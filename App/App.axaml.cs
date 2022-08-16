using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using App.Views;
using Management;
using ReactiveUI;
using Splat;

namespace App
{
    public partial class App : Application
    {
        public App()
        {
            Registry.RegisterVariable("App.TEST.Nice", 69);
            Registry.RegisterVariable("App.TEST.420", 420);
            Registry.RegisterVariable("App.TEST.Temp", "DATA_RANDOM");
            Registry.RegisterVariable("Temp", this);
            Registry.Init();
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
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}