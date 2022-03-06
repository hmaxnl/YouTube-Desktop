using YouTubeGUI.Models;

namespace YouTubeGUI.Stores
{
    public static class WorkplaceStore
    {
        static WorkplaceStore()
        {
            BuildWorkplace();
        }
        public static Workspace DefaultWorkspace { get; set; }
        public static Workspace IncognitoWorkspace { get; set; }

        public static void BuildWorkplace()
        {
            DefaultWorkspace ??= new Workspace(YoutubeUserStore.CurrentUser);
        }
    }
}