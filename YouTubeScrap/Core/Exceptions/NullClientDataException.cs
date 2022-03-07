using System;

namespace YouTubeScrap.Core.Exceptions
{
    public class NullClientDataException : Exception
    {
        public NullClientDataException(string message) : base(message)
        {
            
        }
    }
}