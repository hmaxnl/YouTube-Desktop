using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Core.Exceptions
{
    public class NoUserAuthorizationException : Exception
    {
        public NoUserAuthorizationException(string message) : base(message)
        { }
    }
}