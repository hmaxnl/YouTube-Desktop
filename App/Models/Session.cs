using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YouTubeScrap;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Handlers;

namespace App.Models
{
    public class Session : IDisposable
    {
        public Session(YoutubeUser user, ResponseMetadata? response = null)
        {
            _user = user;
            _responseMetadata = response ?? _user.InitialResponseMetadata;
            GetGuide();
        }
        public void Dispose() => Contents?.Clear();
        public string Title => _responseMetadata.Header.FeedTabbedHeader.Title.ToString();
        public List<Tab> Tabs => (_responseMetadata.Contents != null)
            ? _responseMetadata.Contents.TwoColumnBrowseResultsRenderer.Tabs
            : new List<Tab>();

        public List<object>? Contents { get; } = new List<object>();

        public void GetGuide() => Task.Run(GetGuideTask);

        /// Private

        private readonly ResponseMetadata _responseMetadata;
        private readonly YoutubeUser _user;
        private SessionTarget _sessionTarget;

        private async Task GetGuideTask()
        {
            ResponseMetadata guideResponse = await _user.GetApiMetadataAsync(ApiRequestType.Guide);
            _responseMetadata.Merge(guideResponse);
            FilterResponse();
        }

        private void FilterResponse()
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