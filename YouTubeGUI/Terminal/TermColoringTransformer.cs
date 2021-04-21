using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Media;
using Avalonia.Remote.Protocol.Input;
using AvaloniaEdit.Document;
using AvaloniaEdit.Folding;
using AvaloniaEdit.Rendering;

namespace YouTubeGUI.Terminal
{
    public class TermColoringTransformer : DocumentColorizingTransformer
    {
        // This was the first thing i wanted to implement, but scrapped it and used a rich text model instead.
        // Maybe this will be used in the future.
        
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
                int propLeng = GetFormatData(regexMatch.Value, out string colorFound);
                Match nextMatch = regexMatch.NextMatch();
                int regexIndex = regexMatch.Index;
                ChangeLinePart(line.Offset + regexIndex, nextMatch.Success ? nextMatch.Index + line.Offset : lineText.ToString().Length + line.Offset, (element =>
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