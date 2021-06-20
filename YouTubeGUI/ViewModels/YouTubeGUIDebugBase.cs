using System.Diagnostics;
using Avalonia.Controls;
using AvaloniaEdit;
using YouTubeGUI.View;

using YouTubeScrap;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGuiDebugBase : YouTubeGUIDebug
    {
        private YouTubeLoginBase loginWindow;
        public YouTubeGuiDebugBase()
        {
            loginWindow = new YouTubeLoginBase();
            DataContext = this;
            TextEditor debugTextEditor = this.Find<TextEditor>("DebugTextEditor");
            Terminal.Terminal.TextEditor = debugTextEditor;
        }

        public void DebugCommandTest()
        {
            loginWindow.Show();
        }
    }
}