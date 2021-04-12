using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Handlers
{
    public class DebugHandler
    {
        public DebugHandler()
        {

        }
        public static bool IsDebug =>
#if DEBUG
                true;
#else
                return false;
#endif

    }
}