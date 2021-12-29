using YouTubeGUI.Models.Snippets;

namespace YouTubeGUI.Models
{
    public class HomeModel
    {
        public HomeModel(HomeSnippet? hs, GuideSnippet? gs)
        {
            HomeSnippet = hs;
            GuideSnippet = gs;
        }
        
        public HomeSnippet? HomeSnippet { get; }
        public GuideSnippet? GuideSnippet { get; }
    }
}