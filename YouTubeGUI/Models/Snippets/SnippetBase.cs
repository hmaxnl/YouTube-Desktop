using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Models.Snippets
{
    public class SnippetBase
    {
        protected SnippetBase(ResponseMetadata meta)
        {
            Metadata = meta;
        }

        public ResponseMetadata Metadata { get; }

        public ResponseContext? GetContext => Metadata?.RespContext;
        public FrameworkUpdates? GetFrameworkUpdates => Metadata?.FrameworkUpdates;
        public string? GetTracking => Metadata?.TrackingParams;
    }
}