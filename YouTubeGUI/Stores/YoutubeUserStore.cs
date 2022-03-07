using System;
using System.Collections.Generic;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.Stores
{
    /// <summary>
    /// Store the current user globally to the program.
    /// And some helper functions!
    /// </summary>
    public static class YoutubeUserStore
    {
        public static event Action? NotifyUserChanged;
        public static YoutubeUser CurrentUser
        {
            get => BuildUser();
            set
            {
                _currentUser = value;
                OnUserChanged();
            }
        }

        public static void PreloadUser()
        {
            BuildUser();
        }

        /* Private */
        private static YoutubeUser? _currentUser;
        private static Dictionary<string, YoutubeUser> _users = new Dictionary<string, YoutubeUser>();

        private static void OnUserChanged() => NotifyUserChanged?.Invoke();

        private static YoutubeUser BuildUser() => _currentUser ??= new YoutubeUser();//TODO: Load logged in user, else if no user is logged in create new temp user.
    }
}