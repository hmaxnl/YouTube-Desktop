using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouTubeScrap.Core;

namespace YouTubeScrap.Data.Extend
{
    public class TextElement
    {
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
        [JsonProperty("simpleText")]
        public string SimpleText { get; set; }
        [JsonProperty("runs")]
        public List<TextRun> Runs { get; set; }

        /// <summary>
        /// Get the string.
        /// </summary>
        /// <returns>String stored inside object. If no string is stored return string.Empty</returns>
        public override string ToString()
        {
            if (Runs is { Count: > 0 })
            {
                StringBuilder runBuilder = new StringBuilder();
                foreach (TextRun run in Runs)
                    runBuilder.Append(run.Text);
                return runBuilder.ToString();
            }
            return !SimpleText.IsNullEmpty() ? SimpleText : Accessibility != null ? Accessibility.ToString() : string.Empty;
        }
    }
}