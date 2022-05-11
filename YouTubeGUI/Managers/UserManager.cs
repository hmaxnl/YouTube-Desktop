using System;
using System.Collections.Generic;
using YouTubeScrap;

namespace YouTubeGUI.Managers
{
    //TODO (ddp): Need to be extended for later implementation with the settings, workspaces & sessions.
    /// <summary>
    /// Global user manager.
    /// To keep users manageble.
    /// </summary>
    public static class UserManager
    {
        /// <summary>
        /// Notify if user is changed.
        /// </summary>
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

        public static YoutubeUser BuildUser() => _currentUser ??= new YoutubeUser();//TODO (ddp): Load logged in user, else if no user is defined in create new 'temp' user.
    }
}