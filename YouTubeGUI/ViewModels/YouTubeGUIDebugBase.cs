using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;
using JetBrains.Annotations;
using YouTubeGUI.View;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGUIDebugBase : YouTubeGUIDebug
    {
        public YouTubeGUIDebugBase()
        {
            DataContext = this;
            TextEditor debugTextEditor = this.Find<TextEditor>("DebugTextEditor");
            Terminal.Terminal term = new Terminal.Terminal(debugTextEditor);
            term.AppendNewLine($"Append");
        }
    }
}