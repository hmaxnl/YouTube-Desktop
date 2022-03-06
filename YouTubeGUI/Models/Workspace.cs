using System.Collections.Generic;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.Models
{
    public class Workspace
    {
        /// <summary>
        /// Create a session to use for interacting with youtube and user.
        /// </summary>
        /// <param name="user">The user this session is bound to.</param>
        /// <param name="workspaceState">The state this session will be in.</param>
        public Workspace(YoutubeUser? user = null, WorkspaceState workspaceState = WorkspaceState.LoggedOut)
        {
            WorkspaceUser = user ?? new YoutubeUser();
            State = WorkspaceUser.HasLogCookies ? WorkspaceState.LoggedIn : workspaceState;
            Sessions.Add(new Session(this));// Create the first default workspace
        }
        
        // Properties
        public readonly List<Session> Sessions = new List<Session>();
        public YoutubeUser WorkspaceUser { get; }
        public WorkspaceState State { get; }
    }

    public enum WorkspaceState
    {
        LoggedIn,
        LoggedOut,
        Incognito
    }
}