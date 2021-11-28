using System;
using Avalonia;
using Avalonia.Logging;
using YouTubeGUI.Core;
using YouTubeGUI.Windows;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            Instance = new Bootstrapper(ref DmInstance, args);
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        public static Bootstrapper? Instance;
        public static DebugManager? DmInstance;
        public static MainWindow? MainWindow;
        public static LibVlcManager? LibVlcManager;
        public static YoutubeUser? InitialUser;
        
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
    }
}