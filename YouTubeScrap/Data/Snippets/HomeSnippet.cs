using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Snippets
{
    public class HomeSnippet : SnippetBase
    {
        public List<object> Contents => Tab?.Content.Contents;
        // Maybe there will be more tabs implemented, need to look into how the api response is setup!
        public Tab Tab { get; }

        public HomeSnippet(ResponseMetadata meta) : base(meta)
        {
            if (Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs.Count > 1)
                Trace.WriteLine("There is more than one tab! This is not handled, report this to the developers!");
            foreach (var tab in Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
            {
                Tab = tab;
            }
        }

        public void UpdateContents(ResponseMetadata respMeta)
        {
            // Update the list with the new contents/tabs!
        }
    }
}