using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using YouTubeScrap.Core.Exceptions;

namespace YouTubeScrap.Core
{
    // Will probally use one instance of the handler for the application's lifetime.
    /// <summary>
    /// The network handler that wil handle the outgoing requests to youtube.
    /// </summary>
    internal class NetworkHandler
    {
        // The networkhandler object that caontains everyting of the handler.
        NetworkHandlerSettings _networkHandlerSettings = null;
        // Just a ctor.
        public NetworkHandler(NetworkHandlerSettings networkHandlerSettings = null)
        {
            if (!LibraryHandler.IsNetworkHandlerRegistered)
            {
                if (networkHandlerSettings == null)
                    _networkHandlerSettings = new NetworkHandlerSettings();
                else
                    _networkHandlerSettings = networkHandlerSettings;
            }
            else
                throw new NetworkHandlerNotRegisteredException($"The {nameof(NetworkHandler)} is not registered!");
            LibraryHandler.RegisterHandlerInternal(typeof(NetworkHandler));
        }
        ~NetworkHandler()
        {
            // Cleanup.
            if (_networkHandlerSettings != null)
            {
                _networkHandlerSettings.NetworkHttpClient.Dispose();
                _networkHandlerSettings.NetworkHttpClientHandler.Dispose();
                _networkHandlerSettings = null;
            }
            LibraryHandler.DeregisterHandlerInternal(typeof(NetworkHandler));
        }
    }
    internal class NetworkHandlerSettings
    {
        public HttpClient NetworkHttpClient { get; set; }
        public HttpClientHandler NetworkHttpClientHandler { get; set; }
        public WebProxy NetworkWebProxy { get; set; }
        public ProxySettings NetworkProxySettings { get; set; }
    }
    internal class ProxySettings
    {
        public bool UseProxy { get => _useProxy; set => _useProxy = value; }
        public Uri ProxyAddress { get => _proxyUrl; set => _proxyUrl = value; }
        public int ProxyPort { get => _proxyPort; set => _proxyPort = value; }

        private bool _useProxy = false;
        private Uri _proxyUrl = null;
        private int _proxyPort = 0;
    }
}