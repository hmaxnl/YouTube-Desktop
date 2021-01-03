using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Exceptions;

namespace YouTubeScrap
{
    internal static class LibraryHandler
    {
        // Properties.
        private static bool _isNetworkHandlerRegistered = false;
        public static bool IsNetworkHandlerRegistered { get => _isNetworkHandlerRegistered; }
        

        // Register and Deregister for internal handlers.
        public static void RegisterHandlerInternal(Type handlerType)
        {
            bool isSuccessfull = true;
            switch (handlerType.ToString())
            {
                case nameof(NetworkHandler):
                    if (IsNetworkHandlerRegistered)
                        isSuccessfull = false;
                    else
                        _isNetworkHandlerRegistered = true;
                    break;
                default:
                    // The handler that trys to register is not recognized.
                    break;
            }
            if (!isSuccessfull)
                throw new RegisterConflictException($"The {handlerType.Name} trys to register while a it is already registered!");
        }
        public static void DeregisterHandlerInternal(Type handlerType)
        {
            switch (handlerType.ToString())
            {
                case nameof(NetworkHandler):
                    if (IsNetworkHandlerRegistered)
                        _isNetworkHandlerRegistered = false;
                    break;
            }
        }
    }
}