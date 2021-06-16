using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using YouTubeScrap.Core.Exceptions;
using YouTubeScrap.Core.Youtube;

namespace YouTubeScrap.Handlers
{
    // Using a sealed class to make sure that we have ONE instance running in the application!
    // Use the 'Set' functions to set the properties to the http client, after setting it will auto rebuilt the http client.
    public static class NetworkHandler
    {
        public static string UserAgent { get => "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:85.0) Gecko/20100101 Firefox/85.0"; }
        public static string Origin { get => "https://www.youtube.com"; }
        public static bool IsInitialized { get => client != null; }
        public static HttpClientHandler ClientHandler { get => clientHandler; }

        private static HttpClient client;
        private static HttpClientHandler clientHandler;
        private static WebProxy proxy;

        public static async Task<HttpResponse> MakeRequestAsync(ApiRequest apiRequest, YoutubeUser youtubeUser = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{Origin}{apiRequest.ApiUrl}"),
                Method = apiRequest.Method
            };
            if (apiRequest.Payload != null)
                requestMessage.Content = new StringContent(YoutubeApiManager.CreateJsonRequestPayload(apiRequest), Encoding.UTF8, "application/json");
            if (youtubeUser != null)
            {
                requestMessage.Headers.Add("Cookie", youtubeUser.UserCookies.FinalizedLoginCookies);
                requestMessage.Headers.Authorization = youtubeUser.GenerateAuthentication();
                requestMessage.Headers.Add("Origin", Origin);
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
                proxy = new WebProxy("127.0.0.1", 8888); // For tools like fiddler etc be enable to monitor the requests <Used for debug purposes only>
            else
                proxy = webProxy;
            BuildClientHandler(true);
        }
        public static void BuildClient()
        {
            Trace.WriteLine("Building client...");
            if (clientHandler == null) // Build new
                BuildClientHandler();
            client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        }
        public static void BuildClientHandler(bool setClient = false, HttpClientHandler handler = null)
        {
            Trace.WriteLine("Building client handler");
            if (handler == null)
            {
                clientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    Proxy = proxy,
                    UseProxy = proxy != null,
                    UseCookies = false
                };
            }
            else
                clientHandler = handler;
            if (setClient)
                BuildClient();
        }
        private static async Task<HttpResponse> SendAsync(HttpRequestMessage httpMessage)
        {
            Trace.WriteLine($"Make request to: {httpMessage.RequestUri}");
            HttpResponseMessage response = await client.SendAsync(httpMessage);
            if (response.StatusCode != HttpStatusCode.OK)
                Trace.WriteLine($"<ERROR> {response.StatusCode}");
            var contentResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse() { ResponseString = contentResponse, TimeRequestWasMade = DateTime.Now, HttpResponseMessage = response };
        }
        // Only call on exit application!
        internal static void Dispose()
        {
            clientHandler.Dispose();
            client.Dispose();
        }
    }
    public struct HttpResponse
    {
        public string ResponseString { get; set; }
        public DateTime TimeRequestWasMade { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}