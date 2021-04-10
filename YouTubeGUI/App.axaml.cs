using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Styling;
using YouTubeGUI.ViewModels;

namespace YouTubeGUI
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            YouTubeGUIMainBase mainWindow = new YouTubeGUIMainBase(new MainWindow());
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.MainWindow = mainWindow.LinkedWindow;
            Bootstrap bootstrapper = new Bootstrap(mainWindow);
            base.OnFrameworkInitializationCompleted();
        }
    }
}