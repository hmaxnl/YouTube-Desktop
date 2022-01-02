using Newtonsoft.Json.Linq;

namespace YouTubeScrap.Data.Innertube
{
    public class LanguageDefinitions
    {
        public LanguageDefinitions(JObject langDef)
        {
            LangDefinitions = langDef;
        }

        public JObject LangDefinitions { get; }
    }
}