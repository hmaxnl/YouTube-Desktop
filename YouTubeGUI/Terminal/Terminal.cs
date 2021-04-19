using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Folding;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using AvaloniaEdit.Rendering;
using AvaloniaEdit.Text;
using Brushes = Avalonia.Media.Brushes;
using Color = Avalonia.Media.Color;

namespace YouTubeGUI.Terminal
{
    public static class Terminal
    {
        public static TextEditor TextEditor
        {
            get
            {
                if (_textEditor == null)
                {
                    _textEditor = new TextEditor();
                    _textDocument = _textEditor.Document;
                    return _textEditor;
                }
                return _textEditor;
            }
            set
            {
                _textEditor = value;
                Initialize();
            }
        }
        private static string GetDtString => DateTime.Now.ToString("T");
        private static TextEditor? _textEditor;
        private static TextDocument? _textDocument;
        private static StringBuilder _stringBuilder = new StringBuilder();
        public static void Initialize(bool keepOldDoc = true)
        {
            if (keepOldDoc && _textDocument != null)// Check if we use the current document and if the document is null.
                TextEditor.Document = _textDocument;
            TextEditor.IsReadOnly = true;
            TextEditor.TextArea.TextView.Options.EnableHyperlinks = true;
            TextEditor.TextArea.TextView.Options.EnableEmailHyperlinks = true;
            TextEditor.TextArea.Caret.CaretBrush = Brushes.Transparent;
            TextEditor.TextArea.TextView.LineTransformers.Add(new TermColoringTransformer());
        }

        public static void AppendLog(string txt)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(AppendLine("[", Colors.Blue));
            _stringBuilder.Append(AppendLine(GetDtString, Colors.White));
            _stringBuilder.Append(AppendLine("]", Colors.Blue));
            _stringBuilder.Append('[');
            _stringBuilder.Append(AppendLine("Log", Colors.Firebrick));
            _stringBuilder.Append(AppendLine("]", Colors.Blue));
            _stringBuilder.Append(AppendLine("$", Colors.Magenta));
            _stringBuilder.Append(AppendLine(txt, Colors.Gray));
            _stringBuilder.Append(Environment.NewLine);
            TextEditor.AppendText(_stringBuilder.ToString());
        }

        private static string AppendLine(string txt, Color color)// Gonna make a extension to the string builder.
        {
            return $"&{color.ToString()}>{txt}";
        }
        public static void Append(string msg)
        {
            
        }
    }
}