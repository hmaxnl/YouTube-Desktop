using System;
using System.Threading.Tasks;
using YouTubeGUI.Models.Snippets;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.Models
{
    /// <summary>
    /// Session class to create and manage 'tabs/screens' as sessions to keep everything seperated and manageable.
    /// </summary>
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
            HomeSnippet = new HomeSnippet(Workspace.WorkspaceUser.InitialResponseMetadata);
            TopbarSnippet = new TopbarSnippet(Workspace.WorkspaceUser.InitialResponseMetadata);
            var initGuideReq = await Workspace.WorkspaceUser.GetApiMetadataAsync(ApiRequestType.Guide);
            GuideSnippet = new GuideSnippet(initGuideReq);
            Initialized?.Invoke();
        }
    }
}