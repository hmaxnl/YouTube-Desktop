using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Handlers;

namespace App.Models
{
    public class Session : IDisposable
    {
        public Session(Workspace workspace, ResponseMetadata? response = null)
        {
            _workspace = workspace;
            _responseMetadata = response ?? _workspace.User.InitialResponseMetadata;
            FilterResponse();
        }
        public void Dispose() => Contents?.Clear();
        public string Title => _responseMetadata.Header.FeedTabbedHeader.Title.ToString();
        public List<Tab> Tabs => (_responseMetadata.Contents != null)
            ? _responseMetadata.Contents.TwoColumnBrowseResultsRenderer.Tabs
            : new List<Tab>();

        public List<object>? Contents { get; } = new List<object>();

        public void GetGuide() => Task.Run(GetGuideTask);
        public void FilterResponse() => Task.Run(FilterResponseTask);

        /// Private

        private readonly ResponseMetadata _responseMetadata;
        private readonly Workspace _workspace;
        private SessionTarget _sessionTarget;

        private async void GetGuideTask()
        {
            ResponseMetadata guideResponse = await _workspace.User.GetApiMetadataAsync(ApiRequestType.Guide);
            _responseMetadata.Merge(guideResponse);
            FilterResponse();
        }

        private void FilterResponseTask()
        {
            // Filter contents
            if (_responseMetadata.Contents != null)
            {
                foreach (Tab tab in Tabs)
                {
                    Contents?.AddRange(tab.Content.Contents);
                }
                _responseMetadata.Contents = null;
                _sessionTarget = SessionTarget.Home;
            }
            // Filter continuation responses
            if (_responseMetadata.ResponseReceivedActions != null)
            {
                foreach (ResponseReceivedAction rra in _responseMetadata.ResponseReceivedActions)
                {
                    Contents?.AddRange(rra.ContinuationItemsAction.ContinuationItems);
                }
                _responseMetadata.ResponseReceivedActions = null;
            }
        }
    }

    public enum SessionTarget
    {
        Home,
        PlayList,
        Channel,
        Subscribers,
        Shorts
    }
}