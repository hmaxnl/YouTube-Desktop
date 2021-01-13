using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YouTubeScrap.Core.Exceptions;

namespace YouTubeScrap.Core
{
    // Will probally use one instance of the handler for the application's lifetime.
    /// <summary>
    /// The network handler that wil handle the outgoing requests to youtube.
    /// </summary>
    internal class NetworkHandler : IDisposable
    {
        NetworkHandlerSettings _networkHandlerSettings = null;
        HttpClient P_httpClient { get => _networkHandlerSettings.NetworkHttpClient; }
        // Just a ctor.
        public NetworkHandler(NetworkHandlerSettings networkHandlerSettings = null)
        {
            if (!LibraryHandler.IsNetworkHandlerRegistered)
                _networkHandlerSettings = networkHandlerSettings ?? new NetworkHandlerSettings(); // Checks for settings if nothing is passed we will make a new default setting.
            LibraryHandler.RegisterHandlerInternal(this, out object preRegistered);
            if (preRegistered != null)
                _networkHandlerSettings = preRegistered as NetworkHandlerSettings;
        }
        public NetworkHandlerSettings GetHandlerSettings()
        {
            return _networkHandlerSettings;
        }
        // Cleanup.
        public void Dispose()
        {
            if (_networkHandlerSettings != null)
            {
                _networkHandlerSettings.NetworkHttpClient.Dispose();
                _networkHandlerSettings.NetworkHttpClientHandler.Dispose();
                _networkHandlerSettings = null;
            }
            LibraryHandler.DeregisterHandlerInternal(this, true);
        }
        public async Task<HttpVideoResponse> GetVideoResponseAsync(string videoId)
        {
            ValidateVideoId(videoId);
            string requestUrl = $"https://www.youtube.com/get_video_info?video_id={videoId}&el=detailpage&hl=en";
            var response = await P_httpClient.SendAsync(new HttpRequestMessage() { RequestUri = new Uri(requestUrl), Method = HttpMethod.Get}).ConfigureAwait(false);
            response = response.EnsureSuccessStatusCode();
            return new HttpVideoResponse() { Response = await response.Content.ReadAsStringAsync().ConfigureAwait(false), TimeRequestWasMade = DateTime.Now};
        }
        public async Task<string> GetPlayerSourceResponseAsync(string videoId)
        {
            ValidateVideoId(videoId);
            var playerSourceUrl = await P_httpClient.SendAsync(new HttpRequestMessage() { RequestUri = new Uri(""), Method = HttpMethod.Get}).ConfigureAwait(false);
            playerSourceUrl = playerSourceUrl.EnsureSuccessStatusCode();
            return await playerSourceUrl.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Checks for valid video id else throw exception.
        /// </summary>
        /// <param name="videoId">Video id.</param>
        private void ValidateVideoId(string videoId)
        {
            if (videoId.IsValidVideoId())
                throw new InvalidVideoIdException("The video id is invalid! Could not receive video information."); // Like all the other exception first log it then yeet it. (Still not created the log system)
        }
    }
    internal struct HttpVideoResponse
    {
        public bool ContainsPlayerResponse { get => (!string.IsNullOrEmpty(Response)) && Response.Contains(YouTubeScrapService.PROP_player_response); }
        public bool ContainsWatchNextResponse { get => (!string.IsNullOrEmpty(Response)) && Response.Contains(YouTubeScrapService.PROP_watch_next_response); }
        public string Response { get; set; }
        public DateTime TimeRequestWasMade { get; set; }
    }
    internal class NetworkHandlerSettings
    {
        public HttpClient NetworkHttpClient { get => _networkHttpClient ?? P_setDefaultHttpClient(NetworkHttpClientHandler); set => _networkHttpClient = value; }
        public HttpClientHandler NetworkHttpClientHandler { get => _networkHttpClientHandler ?? P_setDefaultClientHandler(); set => _networkHttpClientHandler = value; }
        public WebProxy NetworkWebProxy { get => _networkWebProxy; set => _networkWebProxy = value; }

        // Why 'P_'?, i get annoyed by the messages that visual studio provide with "Naming rule violation: prefix 'PREFIX'". 'P_' = private.
        private HttpClientHandler P_setDefaultClientHandler()
        {
            return new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate, Proxy = NetworkWebProxy, UseCookies = true, UseProxy = NetworkWebProxy != null};
        }
        private HttpClient P_setDefaultHttpClient(HttpClientHandler httpClientHandler)
        {
            HttpClient httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            return httpClient;
        }

        private HttpClient _networkHttpClient = null;
        private HttpClientHandler _networkHttpClientHandler = null;
        private WebProxy _networkWebProxy = null;
    }
}