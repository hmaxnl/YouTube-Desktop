using System;
using System.Threading.Tasks;
using YouTubeGUI.Models.Snippets;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.Models
{
    public class UserSession
    {
        /// <summary>
        /// Create a session to use for interacting with youtube and user.
        /// </summary>
        /// <param name="user">The user this session is bound to.</param>
        public UserSession(YoutubeUser? user = null)
        {
            _sessionUser = user;
            Task.Run(MakeInitRequest);
        }
        // Properties
        public YoutubeUser SessionUser => _sessionUser ??= new YoutubeUser();// Defaults to new non-logged in user when no user is defined.
        public HomeSnippet? HomeSnippet => _homeSnippet;
        public GuideSnippet? GuideSnippet => _guideSnippet;
        public TopbarSnippet? TopbarSnippet => _topbarSnippet;

        public event Action? MetadataChanged;
        
        // Privates
        private YoutubeUser? _sessionUser;
        private HomeSnippet? _homeSnippet;
        private GuideSnippet? _guideSnippet;
        private TopbarSnippet? _topbarSnippet;
        // session player, etc

        private void InvokeMetadataChanged() => MetadataChanged?.Invoke();
        private async void MakeInitRequest()
        {
            var initHomeReq = await SessionUser.GetApiMetadataAsync(ApiRequestType.Home);
            _homeSnippet = new HomeSnippet(initHomeReq);
            _topbarSnippet = new TopbarSnippet(initHomeReq);
            var initGuideReq = await SessionUser.GetApiMetadataAsync(ApiRequestType.Guide);
            _guideSnippet = new GuideSnippet(initGuideReq);
            InvokeMetadataChanged();
        }
    }

    public enum SessionState
    {
        LoggedIn,
        LoggedOut,
        Incognito
    }
}