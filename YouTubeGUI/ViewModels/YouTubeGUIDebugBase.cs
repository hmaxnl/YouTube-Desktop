using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using AvaloniaEdit;
using YouTubeGUI.View;

using YouTubeScrap;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGuiDebugBase : YouTubeGUIDebug
    {
        public YouTubeGuiDebugBase()
        {
            DataContext = this;
            TextEditor debugTextEditor = this.Find<TextEditor>("DebugTextEditor");
            Terminal.Terminal.TextEditor = debugTextEditor;
        }

        public void DebugCommandTest()
        {
            
        }
    }
}