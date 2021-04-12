using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Handlers;

namespace YouTubeScrap
{
    public static class LibraryHandler
    {
        // Properties.
        public static void Initialize()
        {
            Trace.WriteLine("Initializing...");
            //Handlers.NetworkHandler.SetProxy();
            Handlers.NetworkHandler.BuildClient();
            Trace.WriteLine("Initialized!");
        }
        public static void Dispose()
        {
            Trace.WriteLine("Shutting down Youtube Scrap...");
            Handlers.NetworkHandler.Dispose();
        }
    }
}