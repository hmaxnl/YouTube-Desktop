using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Core.Exceptions
{
    public class NetworkHandlerNotRegisteredException : Exception
    {
        public NetworkHandlerNotRegisteredException(string message) : base(message)
        { }
    }
}