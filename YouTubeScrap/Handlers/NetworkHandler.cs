using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Exceptions;

namespace YouTubeScrap.Handlers
{
    public class NetworkHandler
    {
        public bool IsInitialized => _client != null;

        private readonly HttpClient _client;
        private readonly HttpClientHandler _clientHandler;
        private readonly YoutubeUser _user;

        public NetworkHandler(YoutubeUser user)
        {
            _user = user;
            Log.Information("Building network client...");
            _clientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                Proxy = _user.UserProxy,
                UseProxy = _user.UserProxy != null,
                UseCookies = true,
                CookieContainer = _user.UserCookieContainer
            };
            _client = new HttpClient(_clientHandler);
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(DataManager.NetworkData.UserAgent);
        }
        //==============================
        // Public functions
        //==============================
        public async Task<HttpResponse> MakeApiRequestAsync(ApiRequest apiRequest)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{DataManager.NetworkData.Origin}{apiRequest.ApiUrl}"),
                Method = apiRequest.Method,
                Content = (apiRequest.Payload != null) ? new StringContent(apiRequest.Payload.ToString(), Encoding.UTF8, "application/json") : null
            };
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Headers.IfModifiedSince = new DateTimeOffset(DateTime.Now);
            if (_user.HasLogCookies)
            {
                requestMessage.Headers.Authorization = _user.GenerateAuthentication();
                requestMessage.Headers.Add("Origin", DataManager.NetworkData.Origin);
            }
            else if (apiRequest.RequireAuthentication)
                throw new NoUserAuthorizationException("The request requires authorization but there is no user data or cookies available!");
            HttpRequest request = new HttpRequest()
            {
                Message = requestMessage,
                ContentType = apiRequest.ContentType
            };
            return await SendAsync(request);
        }
        // For fetching the player script that contains the decipher functions.
        public async Task<HttpResponse> GetPlayerScriptAsync(string scriptUrl)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(scriptUrl),
                Method = HttpMethod.Get
            };
            return await SendAsync(new HttpRequest(){ Message = requestMessage, ContentType = ResponseContentType.JS}).ConfigureAwait(false);
        }
        public async Task<byte[]> GetDataAsync(string url) // Downloading raw data.
        {
            try
            {
                return await _client.GetByteArrayAsync(url);
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception while getting data!");
                return null;
            }
        }
        // Only call on exit application!
        public void Dispose()
        {
            _clientHandler.Dispose();
            _client.Dispose();
        }
        //==============================
        // Private functions
        //==============================
        private async Task<HttpResponse> SendAsync(HttpRequest httpRequest)
        {
            Log.Information("Make request to: {requestUrl}", httpRequest.Message.RequestUri);
            HttpResponseMessage response = await _client.SendAsync(httpRequest.Message);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Warning("The request failed!, With status code: {statusCode}", response.StatusCode);
                // For testing!
                /*string respString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                JObject jObjectError = JObject.Parse(respString);*/
                return new HttpResponse();
            }
            Log.Information("Request: {reqUrl} received wit HTTP code: {statusCode}", httpRequest.Message.RequestUri, response.StatusCode);
            var contentResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse() { ResponseString = contentResponse, HttpResponseMessage = response, ContentType = httpRequest.ContentType};
        }
    }

    public struct HttpRequest
    {
        public HttpRequestMessage Message { get; set; }
        public ResponseContentType ContentType { get; set; }
    }
    public struct HttpResponse //TODO: Check if this struct is necessary, else remove it and use only the 'HttpResponseMessage' class.
    {
        public string ResponseString { get; set; }
        public ResponseContentType ContentType { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}