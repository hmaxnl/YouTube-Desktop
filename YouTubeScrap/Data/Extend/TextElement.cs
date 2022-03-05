using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouTubeScrap.Core;

namespace YouTubeScrap.Data.Extend
{
    public class TextElement
    {
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; } = new Accessibility();
        [JsonProperty("simpleText")]
        public string SimpleText { get; set; }
        [JsonProperty("runs")]
        public List<TextRun> Runs { get; set; }
        public string GetText
        {
            get
            {
                if (Runs is { Count: > 0 })
                {
                    StringBuilder runBuilder = new StringBuilder();
                    foreach (TextRun run in Runs)
                        runBuilder.Append(run.Text);
                    return runBuilder.ToString();
                }
                return !SimpleText.IsNullEmpty() ? SimpleText : Accessibility.GetText;
            }
        }

        public override string ToString() => GetText;
    }
}