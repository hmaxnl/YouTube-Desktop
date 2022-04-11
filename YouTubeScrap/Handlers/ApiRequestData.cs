using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Handlers
{
    public class ApiRequestDataData : IApiRequestData
    {
        public ApiRequestDataData(CommandMetadata? metadata, object? endpoint)
        {
            CommandMetadata = metadata;
            Endpoint = endpoint;
        }
        public CommandMetadata? CommandMetadata { get; }
        public object? Endpoint { get; }
    }
}