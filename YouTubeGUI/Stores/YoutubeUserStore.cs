using System;
using YouTubeGUI.Models.Snippets;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.Stores
{
    /// <summary>
    /// Store the current user globally to the program.
    /// And some helper functions!
    /// </summary>
    public static class YoutubeUserStore
    {
        public static event Action? NotifyUserChanged;
        public static event Action<HomeSnippet, GuideSnippet>? NotifyInitialRequestFinished;

        public static YoutubeUser CurrentUser
        {
            get => _currentUser ??= new YoutubeUser();
            set
            {
                _currentUser = value;
                OnUserChanged();
            }
        }

        public static async void MakeInitRequest()
        {
            var initHomeReq = await CurrentUser.GetApiMetadataAsync(ApiRequestType.Home);
            var initialHomeSnippet = new HomeSnippet(initHomeReq);
            var initGuideReq = await CurrentUser.GetApiMetadataAsync(ApiRequestType.Guide);
            var initialGuideSnippet = new GuideSnippet(initGuideReq);
            OnInitialRequestFinished(initialHomeSnippet, initialGuideSnippet);
        }
        
        private static YoutubeUser? _currentUser;

        private static void OnUserChanged() => NotifyUserChanged?.Invoke();
        private static void OnInitialRequestFinished(HomeSnippet hs, GuideSnippet gs) => NotifyInitialRequestFinished?.Invoke(hs, gs);
    }
}