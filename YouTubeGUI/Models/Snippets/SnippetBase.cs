using System;
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

        private ResponseMetadata? _metadata;
        protected ResponseMetadata? Metadata
        {
            get => _metadata;
            set
            {
                _metadata = value;
                MetaChanged?.Invoke();
            }
        }
        
        public event Action? MetaChanged;

        public ResponseContext? GetContext => Metadata?.RespContext;
        public FrameworkUpdates? GetFrameworkUpdates => Metadata?.FrameworkUpdates;
        public string? GetTracking => Metadata?.TrackingParams;
    }
}