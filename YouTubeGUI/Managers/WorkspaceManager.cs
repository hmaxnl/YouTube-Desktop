using System.Collections.Generic;
using Serilog;
using YouTubeGUI.Models;
using YouTubeScrap;

namespace YouTubeGUI.Managers
{
    /// <summary>
    /// Global workspace handling.
    /// This class is used to handle sessions and users.
    /// </summary>
    public static class WorkspaceManager
    {
        static WorkspaceManager()
        {
            if (_defaultWorkspace == null)
                BuildWorkplace(UserManager.CurrentUser);
        }

        public static readonly Dictionary<YoutubeUser, Workspace> Workspaces = new Dictionary<YoutubeUser, Workspace>();
        
        public static Workspace DefaultWorkspace
        {
            get
            {
                if (_defaultWorkspace == null)
                    BuildWorkplace(UserManager.CurrentUser);
                return _defaultWorkspace;
            }
        }
        private static Workspace? _defaultWorkspace;

        public static Workspace? IncognitoWorkspace
        {
            get
            {
                if (_incognitoWorkspace == null)
                    BuildWorkplace(UserManager.CurrentUser, WorkspaceState.Incognito);
                return _incognitoWorkspace;
            }
        }
        private static Workspace? _incognitoWorkspace;

        public static void BuildWorkplace(YoutubeUser user, WorkspaceState state = WorkspaceState.LoggedOut, bool buildNew = false)
        {
            if (Workspaces.Count != 0 && !buildNew)
            { if (!Workspaces.ContainsKey(user)) CreateWorkspace(user, state); }
            else CreateWorkspace(user, state);
            
            switch (state)
            {
                case WorkspaceState.Incognito:
                    _incognitoWorkspace ??= GetWorkspace(user);
                    break;
                default:
                    _defaultWorkspace ??= GetWorkspace(user);
                    break;
            }
        }

        public static Workspace? GetWorkspace(YoutubeUser user) => Workspaces.TryGetValue(user, out Workspace? workspace) ? workspace : null;

        private static void CreateWorkspace(YoutubeUser user, WorkspaceState state)
        {
            Log.Information("Creating workspace!");
            Workspaces.Add(user, new Workspace(user, state));
        }
        
        public static void DestroyWorkspace(Workspace workspace) => DestroyWorkspace(workspace.WorkspaceUser);

        public static void DestroyWorkspace(YoutubeUser user)
        {
            if (!Workspaces.ContainsKey(user)) return;
            Workspaces[user].Dispose();
            Workspaces.Remove(user);
        }
    }
}