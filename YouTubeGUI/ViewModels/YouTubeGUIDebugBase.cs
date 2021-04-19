using Avalonia.Controls;
using AvaloniaEdit;
using YouTubeGUI.View;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGUIDebugBase : YouTubeGUIDebug
    {
        public YouTubeGUIDebugBase()
        {
            DataContext = this;
            TextEditor debugTextEditor = this.Find<TextEditor>("DebugTextEditor");
            Terminal.Terminal.TextEditor = debugTextEditor;
        }
    }
}