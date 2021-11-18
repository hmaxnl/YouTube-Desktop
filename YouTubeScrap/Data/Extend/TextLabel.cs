using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouTubeScrap.Core;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Extend
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class TextLabel
    {
        [JsonProperty("accessibility")]
        public Accessibility Accessibility { get; set; }
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
    }
}