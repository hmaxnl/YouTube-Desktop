using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Handlers
{
    public interface IApiRequestData
    {
        public CommandMetadata? CommandMetadata { get; }
        public object? Endpoint { get; }
    }
}