using System;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Highlighting;

namespace YouTubeGUI.Core
{
    public class LogTerminal
    {
        public TextEditor TextEditor
        {
            get => _textEditor;
            set
            {
                _textEditor = value;
                TextEditor.WordWrap = false;
                TextEditor.IsReadOnly = true;
                TextEditor.Foreground = Brushes.White;
                TextEditor.Document = _document;
                TextEditor.TextArea.TextView.LinkTextForegroundBrush = new SolidColorBrush(Colors.LightBlue, 1);
                TextEditor.TextArea.TextView.Options.EnableHyperlinks = true;
                TextEditor.TextArea.TextView.Options.EnableEmailHyperlinks = true;
                TextEditor.TextArea.TextView.Options.EnableVirtualSpace = true;
                TextEditor.TextArea.Caret.CaretBrush = Brushes.Transparent;
                TextEditor.TextArea.TextView.LineTransformers.Add(new RichTextColorizer(_richTextModel));
            }
        }

        private bool IsInitialized => _textEditor != null;
        private TextEditor? _textEditor;
        private readonly RichTextModel _richTextModel = new RichTextModel();
        private readonly TextDocument _document = new TextDocument();
        
        private readonly Color _sqBracketColor = Colors.Gray;
        private readonly Color _mainForeColor = Colors.White;

        public void AppendLog(string? message)
        {
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.InvokeAsync(() => AppendLog(message), DispatcherPriority.Normal);
                return;
            }
            try
            {
                _document.Insert(_document.TextLength, message);
            }
            catch (Exception e)
            {
            }
        }
    }
}