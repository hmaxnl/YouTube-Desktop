using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeScrap.Core.Exceptions
{
    public class InvalidVideoIdException : Exception
    {
        public InvalidVideoIdException(string message) : base(message)
        { }
    }
}