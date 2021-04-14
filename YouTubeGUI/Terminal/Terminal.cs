using System;
using System.IO;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;
using AvaloniaEdit.Text;

namespace YouTubeGUI.Terminal
{
    public class Terminal : IDisposable
    {
        private readonly TextView _textView;
        private TextEditorOptions _textEditorOptions;
        private readonly TextDocument _textDocument;
        public TextEditorOptions EditorOptions
        {
            get => _textEditorOptions;
            set => _textEditorOptions = value;
        }
        public Terminal(TextView textView)
        {
            _textEditorOptions = new TextEditorOptions() { EnableHyperlinks = true, EnableEmailHyperlinks = true};
            _textDocument = new TextDocument();
            _textView = textView;
            _textView.Options = _textEditorOptions;
            _textView.IsEnabled = false;
            _textView.Document = _textDocument;
            
        }

        public void AppendNewLine(string text)
        {
            
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}