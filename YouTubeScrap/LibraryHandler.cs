using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using YouTubeScrap.Core;

namespace YouTubeScrap
{
    internal static class LibraryHandler
    {
        // Properties.
        private static NetworkHandlerSettings _networkHandlerSettings = null;
        public static bool IsNetworkHandlerRegistered { get => _networkHandlerSettings != null; }

        // Register and Deregister for internal handlers.
        internal static void RegisterHandlerInternal(object handler, out object preRegistered)
        {
            preRegistered = null;
            switch (handler.GetType().Name)
            {
                case nameof(NetworkHandler):
                    if (IsNetworkHandlerRegistered)
                        preRegistered = _networkHandlerSettings;
                    else
                    {
                        NetworkHandler nHand = handler as NetworkHandler;
                        _networkHandlerSettings = nHand.GetHandlerSettings();
                    }
                    break;
                default:
                    // The handler that trys to register is not recognized.
                    preRegistered = null;
                    break;
            }
        }
        internal static void DeregisterHandlerInternal(object handler, bool isCalledFromDispose = false)
        {
            switch (handler.GetType().Name)
            {
                case nameof(NetworkHandler):
                    if (IsNetworkHandlerRegistered)
                    {
                        _networkHandlerSettings = null;
                        if (!isCalledFromDispose)
                        {
                            NetworkHandler nHandler = handler as NetworkHandler;
                            nHandler.Dispose();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}