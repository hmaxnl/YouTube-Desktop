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
            get => _currentUser ??= new YoutubeUser(); //TODO: Load logged in user, else if no user is logged in create new temp user.
            set
            {
                _currentUser = value;
                OnUserChanged();
            }
        }
        
        // Private stuff
        private static YoutubeUser? _currentUser;
        // If more users are logged in store them.
        private static Dictionary<string, YoutubeUser> _users = new Dictionary<string, YoutubeUser>();

        private static void OnUserChanged() => NotifyUserChanged?.Invoke();
    }
}