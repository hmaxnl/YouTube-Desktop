using System;
using System.Collections.Generic;
using YouTubeScrap;

namespace App.Models
{
    public class Workspace : IDisposable
    {
        public Workspace(YoutubeUser user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user), "Argument cannot be null!");
            Pages.Add(new Page(this));
        }
        public YoutubeUser User { get; }

        public readonly HashSet<Page> Pages = new HashSet<Page>();
        
        public void Dispose()
        {
            foreach (Page page in Pages)
            {
                page.Dispose();
            }
        }

        public override int GetHashCode() => User.GetHashCode();
        public override bool Equals(object? obj) => obj != null && User.GetHashCode().Equals(obj.GetHashCode());
        public override string ToString() => $"{nameof(Workspace)}_{User.GetHashCode()}";
    }
}