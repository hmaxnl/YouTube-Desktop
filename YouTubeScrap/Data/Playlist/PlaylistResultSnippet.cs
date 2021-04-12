using System;
using System.Collections.Generic;
using System.Text;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Playlist
{
    public class PlaylistResultSnippet : IContent
    {
        public ContentIdentifier Kind { get; set; }
        public string TrackingParams { get; set; }

        public Type Identifier { get => GetType(); }
    }
}