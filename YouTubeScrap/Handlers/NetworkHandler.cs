using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Exceptions;
using YouTubeScrap.Core.Youtube;

namespace YouTubeScrap.Handlers
{
    // Use the 'Set' functions to set the properties to the http client, after setting it will auto rebuilt the http client.
    internal static class NetworkHandler
    {
        public static bool IsInitialized { get => _client != null; }
        public static HttpClientHandler ClientHandler { get => _clientHandler; }
        public static NetworkHandlerData NetworkHandlerData;
        
        private static HttpClient _client;
        private static HttpClientHandler _clientHandler;
        private static WebProxy _proxy;

        public static async Task<HttpResponse> MakeApiRequestAsync(ApiRequest apiRequest, YoutubeUser youtubeUser = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{NetworkHandlerData.Origin}{apiRequest.ApiUrl}"),
                Method = apiRequest.Method
            };
            if (apiRequest.Payload != null)
                requestMessage.Content = new StringContent(YoutubeApiManager.CreateJsonRequestPayload(apiRequest), Encoding.UTF8, "application/json");
            if (youtubeUser != null)
            {
                requestMessage.Headers.Add("Cookie", youtubeUser.UserCookies.FinalizedLoginCookies);
                requestMessage.Headers.Authorization = youtubeUser.GenerateAuthentication();
                requestMessage.Headers.Add("Origin", NetworkHandlerData.Origin);
            }
            else if (apiRequest.RequireAuthentication)
                throw new NoUserAuthorizationException("The request requires authorization but there is no user data or cookies available!");
            requestMessage.Headers.Add("X-YouTube-Client-Name", "1");
            requestMessage.Headers.Add("X-YouTube-Client-Version", "2.20210215.07.00");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Headers.IfModifiedSince = new DateTimeOffset(DateTime.Now);
            var response = await SendAsync(requestMessage).ConfigureAwait(false);
            return response;
        }

        public static HttpResponse MakeRequest(string url)
        {
            HttpRequestMessage message = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            Task<HttpResponse> requestTask = Task.Run(async () => await SendAsync(message));
            return requestTask.Result;
        }
        private static async Task<HttpResponse> SendAsync(HttpRequestMessage httpMessage)
        {
            Trace.WriteLine($"Make request to: {httpMessage.RequestUri}");
            HttpResponseMessage response = await _client.SendAsync(httpMessage);
            if (response.StatusCode != HttpStatusCode.OK)
                Trace.WriteLine($"<ERROR> {response.StatusCode}");
            var contentResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse() { ResponseString = contentResponse, TimeRequestWasMade = DateTime.Now, HttpResponseMessage = response };
        }
        // Used for getting a hold on the js script that contains the decipher function. 
        public static async Task<HttpResponse> GetPlayerScript(string scriptUrl)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(scriptUrl),
                Method = HttpMethod.Get 
            };
            return await SendAsync(requestMessage).ConfigureAwait(false);
        }
        /// <summary>
        /// Set the proxy to use.
        /// </summary>
        /// <param name="webProxy">The proxy, if null it will set it to local proxy.</param>
        public static void SetProxy(WebProxy webProxy = null)
        {
            Trace.WriteLine("Setting up proxy");
            if (webProxy == null)
                _proxy = new WebProxy("127.0.0.1", 8888); // For tools like fiddler etc be enable to monitor the requests <Used for debug purposes only>
            else
                _proxy = webProxy;
            BuildClientHandler(true);
        }
        private static void BuildClient()
        {
            Trace.WriteLine("Building client...");
            if (_clientHandler == null)
                BuildClientHandler();
            _client = new HttpClient(_clientHandler);
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(NetworkHandlerData.UserAgent);
        }
        private static void BuildClientHandler(bool buildClient = false, HttpClientHandler handler = null)
        {
            Trace.WriteLine("Building client handler");
            if (handler == null)
            {
                _clientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    Proxy = _proxy,
                    UseProxy = _proxy != null,
                    UseCookies = false
                };
            }
            else
                _clientHandler = handler;
            if (buildClient)
                BuildClient();
        }
        // Construct the NetworkHandler to use.
        public static void Construct()
        {
            NetworkHandlerData.Origin = "https://www.youtube.com";
            NetworkHandlerData.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:85.0) Gecko/20100101 Firefox/85.0";
            
            
            BuildClient();
        }
        // Only call on exit application!
        internal static void Dispose()
        {
            _clientHandler.Dispose();
            _client.Dispose();
        }
    }
    public struct HttpResponse
    {
        public string ResponseString { get; set; }
        public DateTime TimeRequestWasMade { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
    internal struct NetworkHandlerData
    {
        public string UserAgent { get; set; }
        public string Origin { get; set; }
    }
}