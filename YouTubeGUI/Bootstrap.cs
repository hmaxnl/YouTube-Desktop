using Avalonia.Controls;
using YouTubeGUI.GUI;

namespace YouTubeGUI
{
    // Bootstrapperino
    public class Bootstrap
    {
        private readonly IWindowBase _mainWindow;
        public Bootstrap(IWindowBase mainWindowBase)
        {
            _mainWindow = mainWindowBase;
        }
    }
}