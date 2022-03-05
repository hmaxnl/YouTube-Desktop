using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Models.Snippets
{
    public class TopbarSnippet : SnippetBase
    {
        public TopbarSnippet(ResponseMetadata meta) : base(meta)
        {
        }

        public Topbar? Topbar => Metadata?.Topbar;
    }
}