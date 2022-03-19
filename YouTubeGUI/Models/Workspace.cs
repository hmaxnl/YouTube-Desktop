using System;
using System.Collections.Generic;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.Models
{
    public class Workspace : IDisposable
    {
        /// <summary>
        /// Create a session to use for interacting with youtube and user.
        /// </summary>
        /// <param name="user">The user this session is bound to.</param>
        /// <param name="workspaceState">The state this session will be in.</param>
        public Workspace(YoutubeUser user, WorkspaceState workspaceState = WorkspaceState.LoggedOut)
        {
            WorkspaceUser = user;
            State = WorkspaceUser.HasLogCookies ? WorkspaceState.LoggedIn : workspaceState;
            Sessions.Add(new Session(this));// Create the first default workspace
        }
        
        // Properties
        public readonly List<Session> Sessions = new List<Session>();
        /// <summary>
        /// User used by the workspace, to make calls to YouTube.
        /// </summary>
        public YoutubeUser WorkspaceUser { get; }
        public WorkspaceState State { get; }

        public void Dispose()
        {
            Dispose(false);
        }
        public void Dispose(bool disposeUser)
        {
            Sessions.Clear();
            if (disposeUser)
                WorkspaceUser.Dispose();
        }
    }

    public enum WorkspaceState
    {
        LoggedIn,
        LoggedOut,
        Incognito
    }
}