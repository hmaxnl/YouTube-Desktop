using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Snippets
{
    public class SnippetBase : ISnippet
    {
        protected SnippetBase(ResponseMetadata meta)
        {
            Metadata = meta;
        }

        public ResponseMetadata Metadata { get; }

        public ResponseContext GetContext => Metadata?.RespContext;
        public FrameworkUpdates GetFrameworkUpdates => Metadata?.FrameworkUpdates;
        public string GetTracking => Metadata?.TrackingParams;
    }
}