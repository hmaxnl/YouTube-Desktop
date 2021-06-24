using Chromely;
using Chromely.Core;
using Chromely.Core.Configuration;
using Chromely.Core.Host;

namespace ChromelyBrowser
{
    public class BrowserWindow : Window
    {
        public BrowserWindow(IChromelyNativeHost nativeHost, IChromelyConfiguration config, ChromelyHandlersResolver handlersResolver) : base(nativeHost, config, handlersResolver)
        {
        }
    }
}