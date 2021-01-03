using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Core.Exceptions
{
    public class RegisterConflictException : Exception
    {
        public RegisterConflictException(string message) : base(message)
        { }
    }
}