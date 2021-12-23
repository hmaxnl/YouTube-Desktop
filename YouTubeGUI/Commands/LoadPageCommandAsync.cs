using System;
using System.Threading.Tasks;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.Commands
{
    public class LoadPageCommandAsync : AsyncCommandBase
    {
        private readonly ApiRequestType _requestType;
        private readonly YoutubeUser _youtubeUser;
        private readonly Action<ResponseMetadata> _updateAction;
        public LoadPageCommandAsync(ApiRequestType request, YoutubeUser youtubeUser, Action<ResponseMetadata> updateAction)
        {
            _requestType = request;
            _updateAction = updateAction;
            _youtubeUser = youtubeUser;
        }
        public override async Task ExecuteAsync()
        {
            var responseMeta = await _youtubeUser.GetApiMetadataAsync(_requestType);
            _updateAction(responseMeta);
        }
    }
}