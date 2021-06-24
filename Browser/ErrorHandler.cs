using System;
using System.IO;
using Chromely.Core;
using Chromely.Core.Network;

namespace Browser
{
    public class ErrorHandler : IChromelyErrorHandler
    {
        public IChromelyResponse HandleRouteNotFound(string requestId, string routePath)
        {
            throw new NotImplementedException();
        }

        public IChromelyResource HandleError(FileInfo fileInfo, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public IChromelyResource HandleError(Stream stream, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public IChromelyResponse HandleError(IChromelyRequest request, Exception exception)
        {
            throw new NotImplementedException();
        }

        public IChromelyResponse HandleError(IChromelyRequest request, IChromelyResponse response, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}