using YouTubeGUI.Models;

namespace YouTubeGUI.Stores
{
    public static class SessionStore
    {
        static SessionStore()
        {
            LoadSession();
        }
        public static UserSession DefaultSession { get; set; }
        public static UserSession IncognitoSession { get; set; }

        public static void LoadSession()
        {
            DefaultSession ??= new UserSession(YoutubeUserStore.CurrentUser);
        }
    }
}