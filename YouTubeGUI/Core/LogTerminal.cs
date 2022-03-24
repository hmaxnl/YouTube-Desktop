using System;
using System.Diagnostics;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaEdit;
using AvaloniaEdit.Highlighting;
using YouTubeScrap.Core;

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
        
        private readonly Color _sqBracketColor = Colors.Gray;
        private readonly Color _mainForeColor = Colors.White;

        public void AppendLog(string? txt, LogType logType = LogType.Info, Exception? ex = null, StackTrace? stackTrace = null, string caller = "", string uLib = "")
        {
            if (!IsInitialized) return;
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.InvokeAsync(() => AppendLog(txt, logType, ex, stackTrace, caller, uLib), DispatcherPriority.Normal);
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
                    Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
                    Append(new RtbProperties() { Text = callerName, Foreground = Colors.Chocolate });
                    Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
                }
            }
            if (!uLib.IsNullEmpty())
            {
                Color textCol = uLib.ToUpper().Contains("LIBVLC") ? Colors.OrangeRed : Colors.Pink;
                Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
                Append(new RtbProperties() { Text = uLib, Foreground = textCol });
                Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
            }
            if (!caller.IsNullEmpty())
            {
                Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
                Append(new RtbProperties() { Text = caller, Foreground = Colors.SteelBlue });
                Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
            }
            Append(new RtbProperties() { Text = "> ", Foreground = _mainForeColor });
            Append(new RtbProperties() { Text = txt, Foreground = _mainForeColor, NewLine = logType != LogType.Exception });
            if (logType == LogType.Exception && ex != null)
            {
                TextEditor.AppendText(Environment.NewLine);
                Append(new RtbProperties() { Text = $"\t{ex.Message}", Foreground = Colors.Red, Background = Colors.Yellow, NewLine = true });
            }
            TextEditor.ScrollToEnd();
        }

        private void AppendDateTime()
        {
            Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
            Append(new RtbProperties() { Text = DebugManager.GetDateTimeNow, Foreground = _mainForeColor });
            Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
        }
        private void AppendLogType(LogType logType)
        {
            Color colorToUse = logType switch
            {
                LogType.Info => Colors.GreenYellow,
                LogType.Trace => Colors.DodgerBlue,
                LogType.Warning => Colors.Yellow,
                LogType.Error => Colors.Orange,
                LogType.Exception => Colors.Red,
                LogType.Debug => Colors.Brown,
                _ => _mainForeColor
            };
            Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
            Append(new RtbProperties() { Text = logType.ToString(), Foreground = colorToUse });
            Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
        }

        private void Append(RtbProperties rtbProperties)
        {
            if (!IsInitialized) return;
            TextEditor.AppendText(rtbProperties.Text + (rtbProperties.NewLine ? Environment.NewLine : string.Empty));
            TextEditor.ScrollToEnd();
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
            public Color Foreground;
            public Color Background;
            public FontStyle FontStyle = FontStyle.Normal;
            public FontWeight FontWeight = FontWeight.Normal;
            public bool NewLine = false;
        }
    }
}