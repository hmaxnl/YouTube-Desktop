using System;
using System.Diagnostics;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Highlighting;
using Brushes = Avalonia.Media.Brushes;
using Color = Avalonia.Media.Color;
using FontStyle = Avalonia.Media.FontStyle;

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

        public static void AppendLog(string? txt, LogType logType = LogType.Log, Exception? ex = null, StackTrace? stackTrace = null)
        {
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.InvokeAsync(() => AppendLog(txt, logType, ex, stackTrace), DispatcherPriority.Normal);
                return;
            }
            AppendDateTime();
            AppendLogType(logType);
            if (logType == LogType.Trace && stackTrace != null)
            {
#pragma warning disable 8602
                string callerName = stackTrace.GetFrame(3).GetMethod().DeclaringType.Name;
#pragma warning restore 8602
                if (callerName != string.Empty)
                {
                    Append(new RtbProperties() { Text = "[", Foreground = SqBracketColor });
                    Append(new RtbProperties() { Text = callerName, Foreground = Colors.Chocolate });
                    Append(new RtbProperties() { Text = "]", Foreground = SqBracketColor });
                }
            }
            Append(new RtbProperties() { Text = "> ", Foreground = MainForeColor });
            Append(new RtbProperties() { Text = txt, Foreground = MainForeColor, NewLine = logType != LogType.Exception });
            if (logType == LogType.Exception && ex != null)
            {
                TextEditor.AppendText(Environment.NewLine);
                Append(new RtbProperties() { Text = $"\t{ex.Message}", Foreground = Colors.Red, Background = Colors.Yellow, NewLine = true });
            }
        }

        private static void AppendDateTime()
        {
            Append(new RtbProperties() { Text = "[", Foreground = SqBracketColor });
            Append(new RtbProperties() { Text = GetDtString, Foreground = MainForeColor });
            Append(new RtbProperties() { Text = "]", Foreground = SqBracketColor });
        }
        private static void AppendLogType(LogType logType)
        {
            Color colorToUse;
            switch (logType)
            {
                case LogType.Log:
                    colorToUse = Colors.GreenYellow;
                    break;
                case LogType.Trace:
                    colorToUse = Colors.DodgerBlue;
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
            Append(new RtbProperties() { Text = "[", Foreground = SqBracketColor });
            Append(new RtbProperties() { Text = logType.ToString(), Foreground = colorToUse });
            Append(new RtbProperties() { Text = "]", Foreground = SqBracketColor });
        }

        public enum LogType
        {
            Log,
            Trace,
            Warning,
            Error,
            Exception
        }

        private static void Append(RtbProperties rtbProperties)
        {
            TextEditor.AppendText(rtbProperties.Text + (rtbProperties.NewLine ? Environment.NewLine : string.Empty));
            if (rtbProperties.Text != null)
            {
                int offset = TextEditor.Text.Length - rtbProperties.Text.Length;
                _richTextModel.SetForeground(offset, rtbProperties.Text.Length, new SimpleHighlightingBrush(rtbProperties.Foreground));
                _richTextModel.SetBackground(offset, rtbProperties.Text.Length, new SimpleHighlightingBrush(rtbProperties.Background));
                _richTextModel.SetFontStyle(offset, rtbProperties.Text.Length, rtbProperties.FontStyle);
                _richTextModel.SetFontWeight(offset, rtbProperties.Text.Length, rtbProperties.FontWeight);
            }
        }

        private class RtbProperties
        {
            public string? Text = string.Empty;
            public Color Foreground = MainForeColor;
            public Color Background;
            public FontStyle FontStyle = FontStyle.Normal;
            public FontWeight FontWeight = FontWeight.Normal;
            public bool NewLine = false;
        }
    }
}