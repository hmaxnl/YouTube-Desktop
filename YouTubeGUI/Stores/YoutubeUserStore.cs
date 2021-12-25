using System;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.Stores
{
    /// <summary>
    /// Store the current user globally to the program, used to make requests outside the "MainViewModel" scope.
    /// </summary>
    public static class YoutubeUserStore
    {
        private static YoutubeUser? _currentUser;

        public static YoutubeUser CurrentUser
        {
            get => _currentUser ??= YoutubeUser.BuildUserAndExecute(YoutubeUser.ReadCookies());
            set
            {
                _currentUser = value;
                OnUserChanged();
            }
        }

        public static event Action? NotifyUserChanged;

        private static void OnUserChanged() => NotifyUserChanged?.Invoke();
    }
}