using System;
using System.Text.RegularExpressions;
using Avalonia.Media;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;

namespace YouTubeGUI.Terminal
{
    public class TermColoringTransformer : DocumentColorizingTransformer
    {
        // Formats
        // Color:
        // "&cforeground_COLOR>" Foreground color.
        // "&cbackground_COLOR" Background color.
        // Font:
        // "&fsize_FONT>" Font size.
        // "&fstyle_FONT>" Font style.
        // "&fweight_FONT>" Font weight.
        // "&ffamily_FONT>" Font family.
        
        protected override void ColorizeLine(DocumentLine line)
        {
            var lineText = CurrentContext.Document.GetText(line).AsSpan();
            Match regexMatch = Regex.Match(lineText.ToString(), "&(.*?)>");// Regex match for finding the formats.
            while (regexMatch.Success)
            {
                GetFormatData(regexMatch.Value, out string colorFound);
                Match nextMatch = regexMatch.NextMatch();
                ChangeLinePart(regexMatch.Index, nextMatch.Success ? nextMatch.Index : lineText.ToString().Length, (element =>
                {
                    element.TextRunProperties.ForegroundBrush = new SolidColorBrush(Color.TryParse(colorFound, out Color colorParsed) ? colorParsed : Colors.Blue);// Try parse color if not set default color!
                }));
                regexMatch = nextMatch;
            }
        }

        private int GetFormatData(string str, out string formatFound)
        {
            formatFound = str.Remove(str.IndexOf("&"), 1);
            formatFound = formatFound.Remove(formatFound.IndexOf(">"));
            return str.Length;
        }
    }
}