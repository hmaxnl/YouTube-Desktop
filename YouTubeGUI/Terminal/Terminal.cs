using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using AvaloniaEdit.Rendering;
using AvaloniaEdit.Text;

namespace YouTubeGUI.Terminal
{
    public class Terminal : IDisposable
    {
        private readonly TextEditor _textEditor;
        private static string GetDtString => DateTime.Now.ToString("T");

        public Terminal(TextEditor textEditor)
        {
            _textEditor = textEditor;
            _textEditor.IsReadOnly = true;
            _textEditor.TextArea.TextView.Options.EnableHyperlinks = true;
            _textEditor.TextArea.TextView.Options.EnableEmailHyperlinks = true;
            _textEditor.TextArea.Caret.CaretBrush = Brushes.Transparent;
            //_textEditor.SyntaxHighlighting.MainRuleSet.Rules.Add();
        }

        public void AppendNewLine(string text)
        {
            _textEditor.AppendText($"&{Colors.Red.ToString()}>[&{Colors.Green.ToString()}>{GetDtString}&{Colors.Red.ToString()}>]&{Colors.Yellow.ToString()}>x> &{Colors.Blue.ToString()}>{text}");
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}