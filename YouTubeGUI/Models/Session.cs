using System;
using System.Threading.Tasks;
using YouTubeGUI.Models.Snippets;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.Models
{
    public class Session
    {
        public Session(Workspace workspace)
        {
            Workspace = workspace;
            Task.Run(InitialRequest);
        }

        public event Action? Initialized;

        /* Snippets */
        public HomeSnippet HomeSnippet { private set; get; }
        public GuideSnippet GuideSnippet { private set; get; }
        public TopbarSnippet TopbarSnippet { private set; get; }

        /* Privates */
        public readonly Workspace Workspace;
        
        private async void InitialRequest()
        {
            var initHomeReq = await Workspace.WorkspaceUser.GetApiMetadataAsync(ApiRequestType.Home);
            HomeSnippet = new HomeSnippet(initHomeReq);
            TopbarSnippet = new TopbarSnippet(initHomeReq);
            var initGuideReq = await Workspace.WorkspaceUser.GetApiMetadataAsync(ApiRequestType.Guide);
            GuideSnippet = new GuideSnippet(initGuideReq);
            Initialized?.Invoke();
        }
    }
}