using System;
using System.Collections.Generic;
using YouTubeScrap;

namespace YouTubeGUI.Stores
{
    /// <summary>
    /// Store the current user globally to the program.
    /// And some helper functions!
    /// </summary>
    public static class UserManager
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

        /* Private */
        private static YoutubeUser? _currentUser;
        private static Dictionary<string, YoutubeUser> _users = new Dictionary<string, YoutubeUser>();

        private static void OnUserChanged() => NotifyUserChanged?.Invoke();

        public static YoutubeUser BuildUser() => _currentUser ??= new YoutubeUser();//TODO: Load logged in user, else if no user is logged in create new temp user.
    }
}