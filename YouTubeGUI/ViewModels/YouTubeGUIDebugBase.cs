using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using AvaloniaEdit;
using YouTubeGUI.View;

using YouTubeScrap;
using YouTubeScrap.Core;
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

        // Ty: https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
        public void DebugCommandTest()
        {
            
        }
    }
}