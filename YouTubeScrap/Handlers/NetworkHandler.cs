using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Exceptions;
using YouTubeScrap.Core.Youtube;

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
            
            Trace.WriteLine("Building client...");
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
        // Used for getting a hold on the js script that contains the decipher function.
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
                Trace.WriteLine($"Exception while getting data; {e.Message}");
                return null;
            }
        }
        private async Task<HttpResponse> SendAsync(HttpRequest httpRequest)
        {
            Trace.WriteLine($"Make request to: {httpRequest.Message.RequestUri}");
            HttpResponseMessage response = await _client.SendAsync(httpRequest.Message);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Trace.WriteLine($"The request failed! Status code:{response.StatusCode}");
                return new HttpResponse();
            }
            Trace.WriteLine($"Request: {httpRequest.Message.RequestUri} received with HTTP code: {response.StatusCode}");
            var contentResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse() { ResponseString = contentResponse, HttpResponseMessage = response, ContentType = httpRequest.ContentType};
        }
        // Only call on exit application!
        public void Dispose()
        {
            _clientHandler.Dispose();
            _client.Dispose();
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