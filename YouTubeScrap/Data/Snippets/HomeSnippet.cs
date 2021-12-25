using System.Collections.Generic;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Snippets
{
    public class HomeSnippet : SnippetBase
    {
        public IEnumerable<object> Contents => Tab?.Content.Contents;
        public Tab Tab { get; }

        public HomeSnippet(ResponseMetadata meta) : base(meta)
        {
            foreach (var tab in Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
            {
                Tab = tab;
            }
        }
    }
}