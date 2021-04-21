using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
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
using SkiaSharp;
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
        private static RichTextModel _richTextModel = new RichTextModel();
        
        private static readonly Color SqBracketColor = Colors.Gray;
        private static readonly Color MainForeColor = Colors.White;
        public static void Initialize(bool keepOldDoc = true)
        {
            if (keepOldDoc && _textDocument != null)// Check if we use the current document and if the document is null.
                TextEditor.Document = _textDocument;
            TextEditor.IsReadOnly = true;
            TextEditor.TextArea.TextView.Options.EnableHyperlinks = true;
            TextEditor.TextArea.TextView.Options.EnableEmailHyperlinks = true;
            TextEditor.TextArea.Caret.CaretBrush = Brushes.Transparent;
            TextEditor.TextArea.TextView.LineTransformers.Add(new RichTextColorizer(_richTextModel));
        }

        public static void AppendLog(string txt, LogType logType = LogType.Log, Exception? ex = null)
        {
            AppendDateTime();
            AppendLogType(logType);
            Append("> ", Colors.LightBlue);
            Append(txt, MainForeColor, new Color(), logType != LogType.Exception);
            if (logType == LogType.Exception && ex != null)
            {
                TextEditor.AppendText(Environment.NewLine);
                Append($"\t{ex.Message}", Colors.Red, Colors.Yellow, true);
            }
        }

        private static void AppendDateTime()
        {
            Append("[", SqBracketColor);
            Append(GetDtString, MainForeColor);
            Append("]", SqBracketColor);
        }
        private static void AppendLogType(LogType logType)
        {
            Color colorToUse;
            switch (logType)
            {
                case LogType.Log:
                    colorToUse = Colors.GreenYellow;
                    break;
                case LogType.Warning:
                    colorToUse = Colors.Yellow;
                    break;
                case LogType.Error:
                    colorToUse = Colors.Orange;
                    break;
                case LogType.Exception:
                    colorToUse = Colors.Red;
                    break;
                default:
                    colorToUse = MainForeColor;
                    break;
            }
            Append("[", SqBracketColor);
            Append(logType.ToString(), colorToUse);
            Append("]", SqBracketColor);
        }

        public enum LogType
        {
            Log,
            Warning,
            Error,
            Exception
        }
        private static void Append(string msg, Color colorForeground, Color colorBackground = new Color(), bool newLine = false)
        {
            TextEditor.AppendText(msg);
            if (newLine)
                TextEditor.AppendText(Environment.NewLine);
            _richTextModel.ApplyHighlighting(TextEditor.Text.Length - msg.Length, msg.Length, new HighlightingColor() { Foreground = new SimpleHighlightingBrush(colorForeground), Background = new SimpleHighlightingBrush(colorBackground)});
        }
    }
}