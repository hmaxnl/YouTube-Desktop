using System;
using Avalonia;
using YouTubeGUI.Core;

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
            Instance = new Bootstrapper(ref WmInstance, ref DmInstance);
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        public static Bootstrapper? Instance;
        public static WindowManager? WmInstance;
        public static DebugManager? DmInstance;
        
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
    }
}