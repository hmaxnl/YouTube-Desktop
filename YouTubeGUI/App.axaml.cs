using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json.Linq;
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
        
        private static JObject? _initialResponse;
        
        [STAThread]
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
            Terminal.Terminal.Initialize();
            Trace.Listeners.Add(new DebugTraceListener());
            SetupDebug();
            
            YouTubeService = new YouTubeService(ref _initialResponse);
            MainWindow = new YouTubeGuiMainBase();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = MainWindow;
            }
            base.OnFrameworkInitializationCompleted();
        }
        
        private static void SetupDebug()
        {
            DebugWindow ??= new YouTubeGuiDebugBase();
            DebugWindow.Title = "...Debug...";
            DebugWindow.Show();
            Terminal.Terminal.AppendLog("Debug mode enabled!");
        }

        public void Shutdown()
        {
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.Shutdown();
        }
    }
}