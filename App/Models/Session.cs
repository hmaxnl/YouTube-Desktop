using System.Collections.Generic;
using System.Threading.Tasks;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Handlers;

namespace App.Models
{
    public class Session
    {
        public Session(Workspace workspace, ResponseMetadata? response = null)
        {
            _workspace = workspace;
            if (response != null)
                _responseMetadata = response;
            else
                _responseMetadata = _workspace.User.InitialResponseMetadata;
        }

        public string Title => _responseMetadata.Header.FeedTabbedHeader.Title.ToString();

        public List<Tab> Tabs => (_responseMetadata.Contents != null)
            ? _responseMetadata.Contents.TwoColumnBrowseResultsRenderer.Tabs
            : new List<Tab>();

        public List<object>? Contents
        {
            get
            {
                if (_contents == null)
                    _contents = new List<object>();
                Task.Run(FilterContentsTask);
                return _contents;
            }
        }

        private List<object>? _contents;

        public void GetGuide() => Task.Run(GetGuideTask);

        /// Private stuff

        private readonly ResponseMetadata _responseMetadata;
        private readonly Workspace _workspace;

        private async void GetGuideTask()
        {
            ResponseMetadata guideResponse = await _workspace.User.GetApiMetadataAsync(ApiRequestType.Guide);
            _responseMetadata.Merge(guideResponse);
        }

        private void FilterContentsTask()
        {
            if (_responseMetadata.Contents != null)
            {
                foreach (Tab tab in Tabs)
                {
                    _contents?.AddRange(tab.Content.Contents);
                }
            }

            if (_responseMetadata.ResponseReceivedActions != null)
            {
                foreach (ResponseReceivedAction rra in _responseMetadata.ResponseReceivedActions)
                {
                    _contents?.AddRange(rra.ContinuationItemsAction.ContinuationItems);
                }
            }
        }
    }
}