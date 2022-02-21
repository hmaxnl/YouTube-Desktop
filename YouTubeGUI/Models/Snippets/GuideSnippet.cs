using System.Collections.Generic;
using JetBrains.Annotations;
using YouTubeScrap.Data;

namespace YouTubeGUI.Models.Snippets
{
    public class GuideSnippet : SnippetBase
    {
        public GuideSnippet([NotNull] ResponseMetadata meta) : base(meta)
        {
        }
        
        public IEnumerable<object> GuideItems => Metadata?.Items;

        public void UpdateContents(ResponseMetadata respMeta)
        {
            
        }
    }
}