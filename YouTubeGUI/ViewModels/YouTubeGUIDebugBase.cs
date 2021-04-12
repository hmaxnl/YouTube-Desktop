using Avalonia.Controls;
using JetBrains.Annotations;
using YouTubeGUI.View;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGUIDebugBase : YouTubeGUIDebug
    {
        public YouTubeGUIDebugBase()
        {
            DataContext = this;
            YouTubeGUIDebug testWinDeb = new YouTubeGUIDebug();
            
        }
    }
}