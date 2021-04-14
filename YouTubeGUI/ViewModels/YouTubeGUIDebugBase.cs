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
        private readonly TextView debugTextBlock;
        public YouTubeGUIDebugBase()
        {
            DataContext = this;
            debugTextBlock = this.Find<TextView>("DebugTextView");
            Terminal.Terminal term = new Terminal.Terminal(debugTextBlock);
            term.AppendNewLine("Test");
            term.AppendNewLine("Test2");
        }
    }
}