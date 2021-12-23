using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.Stores
{
    public class YoutubeUserStore
    {
        private readonly YoutubeUser _currentUser;

        public YoutubeUserStore(YoutubeUser user)
        {
            _currentUser = user;
        }
    }
}