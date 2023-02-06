using System;
using System.Collections.Generic;
using YouTubeScrap;

namespace App.Models
{
    public class Workspace : IDisposable
    {
        public Workspace(YoutubeUser user)
        {
            User = user;
            _sessions.Add(new Session(this)); // Create a new session on start
        }

        private readonly List<Session> _sessions = new List<Session>();
        public readonly YoutubeUser User;
        public WorkspaceState State => User.HasLogCookies ? WorkspaceState.LoggedIn : WorkspaceState.LoggedOut;

        public void Dispose(bool disposeUser)
        {
            _sessions.Clear();
            if (disposeUser)
                User.Dispose();
        }
        public void Dispose() => Dispose(false);
    }

    public enum WorkspaceState
    {
        LoggedIn,
        LoggedOut,
        Anon
    }
}