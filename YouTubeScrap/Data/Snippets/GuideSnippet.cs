using System.Collections.Generic;

namespace YouTubeScrap.Data.Snippets
{
    public class GuideSnippet : SnippetBase
    {
        public IEnumerable<object> GuideItems => Metadata?.Items;
        public GuideSnippet(ResponseMetadata meta) : base(meta)
        { }

        public void UpdateContents(ResponseMetadata respMeta)
        {
            
        }
    }
}