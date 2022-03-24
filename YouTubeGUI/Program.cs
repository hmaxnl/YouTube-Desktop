using System;
using Avalonia;
using YouTubeGUI.Core;
using YouTubeGUI.Windows;

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
        
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().With(new SkiaOptions() { MaxGpuResourceSizeBytes = 8096000 }).UseSkia()
            .LogToTrace();
    }
}