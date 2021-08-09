using System.ComponentModel;
using Avalonia.Controls;
using YouTubeGUI.View;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGuiMainBase : YouTubeGUIMain
    {
        public YouTubeGuiMainBase()
        {
            Terminal.Terminal.AppendLog("Creating main window!");
            DataContext = this;
            Closing += OnClosing;
        }
        private void OnClosing(object? sender, CancelEventArgs e)
        {
            
        }
    }
}