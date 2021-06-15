using System.Diagnostics;
using Avalonia.Controls;
using AvaloniaEdit;
using YouTubeGUI.View;

using YouTubeScrap;
using YouTubeScrap.Core.Youtube;

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

        public void DebugCommandTest()
        {
            YoutubeService ytService = new YoutubeService();
            ytService.TestRequester();
        }
    }
}