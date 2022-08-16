#nullable enable
using System.Collections.Generic;

namespace YouTubeScrap
{
    public static class YouTubeUserManager
    {
        public static YoutubeUser DefaultUser
        {
            get
            {
                _defaultUser ??= new YoutubeUser();
                UsersHash.Add(_defaultUser);
                return _defaultUser;
            }
            set
            {
                if (value == null || value.Equals(_defaultUser)) return;
                _defaultUser = value;
                if (UsersHash.Contains(_defaultUser)) return;
                UsersHash.Add(_defaultUser);
            }
        }
        public static YoutubeUser DefaultIncognitoUser => _defaultIncognitoUser ??= new YoutubeUser();
        public static HashSet<YoutubeUser> UsersHash { get; } = new HashSet<YoutubeUser>();

        private static YoutubeUser? _defaultUser;
        private static YoutubeUser? _defaultIncognitoUser;
    }
}