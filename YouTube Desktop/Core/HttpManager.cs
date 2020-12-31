using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YouTube_Desktop.Core.Models;
using YouTube_Desktop.Core;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YouTube_Desktop.Core
{
    // HttpManager
    /// <summary>
    /// Manager that handles the http calls requests and responses from google/youtube servers.
    /// </summary>
    public class HttpManager : IHttpManager, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;
        // Used for the request urls.
        private readonly Dictionary<int, string> _videoRequestCalls = new Dictionary<int, string>// {0} = video id.
            {
                {0, "https://www.youtube.com/get_video_info?video_id={0}&hl=en"}, // Default url
                {1, "https://www.youtube.com/get_video_info?video_id={0}&el=detailpage&hl=en"}, // Second url for content not playable in embbeded
                {2, "https://www.youtube.com/get_video_info?video_id={0}&el=embedded&hl=en"}
            };
        //private readonly string _playlistAjaxXmlCall = "https://www.youtube.com/list_ajax?style=xml&action_get_list=1&list={0}&index={1}&hl=en"; // {0} = playlist id, {1} = index. | playlist

        public HttpManager()
        {
            // Setup the HttpManager.
            _httpHandler = new HttpClientHandler();
            //HttpRequestMessage HttpRMsg = new HttpRequestMessage();

            // Set some settings.
            if (_httpHandler.SupportsAutomaticDecompression)
                _httpHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            _httpHandler.UseCookies = false;
            _httpClient = new HttpClient(_httpHandler);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
        }
        public async Task<string> GetResponseAsync(string queryUrl)
        {
            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, queryUrl))
            {
                using (var response = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
        }
        /// <summary>
        /// Get a video response.
        /// </summary>
        /// <param name="videoId">The video id.</param>
        /// <param name="decode">If the response needs to be decoded.</param>
        /// <param name="requestUrlIndex">For other request url.</param>
        /// <returns></returns>
        public async Task<string> GetVideoResponseAsync(string videoId, bool decode = true, int requestUrlIndex = 0)
        {
            string requestUrl = string.Format(_videoRequestCalls[requestUrlIndex], videoId);
            string response = await GetResponseAsync(requestUrl).ConfigureAwait(false);
            if (decode)
            {
                response = WebUtility.UrlDecode(response);
                response = response.Replace("\\u0026", "&");
                response = response.Replace("u0026", "&");
            }
            RequestResponse responseCheck = CheckResponse(response);
            return (responseCheck.Successful) ? response : null;
        }
        public RequestResponse CheckResponse(string responseUrl)
        {
            Dictionary<string, string> checkResponseDict = GetDictionaryFromResponse(responseUrl);
            RequestResponse reqRes = new RequestResponse();
            if (checkResponseDict.TryGetValue("status", out string value))
                reqRes.Successful = (value != "fail");
            reqRes.ResponseInfo = checkResponseDict;
            reqRes.UrlResponse = responseUrl;
            return reqRes;
        }
        private Dictionary<string, string> GetDictionaryFromResponse(string response)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string[] splitted = response.Split('&');
            foreach (var KeyVal in splitted)
            {
                string decoded = WebUtility.UrlDecode(KeyVal);
                int equalPostCount = decoded.IndexOf('=');
                if (equalPostCount <= 0)
                    continue;
                string key = decoded.Substring(0, equalPostCount);
                string val = equalPostCount < decoded.Length ? decoded.Substring(equalPostCount + 1) : string.Empty;
                dictionary[key] = val;
            }
            return dictionary;
        }
        private Dictionary<string, string> GetDictionaryFromResponses(string[] responses)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            IList<string> splittedResponses = new List<string>();
            foreach (var response in responses)
            {
                string[] tempSplit = response.Split('&');
                foreach (var split in tempSplit)
                    splittedResponses.Add(split);
            }
            foreach (var KeyVal in splittedResponses)
            {
                string decoded = WebUtility.UrlDecode(KeyVal);
                int equalPostCount = decoded.IndexOf('=');
                if (equalPostCount <= 0)
                    continue;
                string key = decoded.Substring(0, equalPostCount);
                string val = equalPostCount < decoded.Length ? decoded.Substring(equalPostCount + 1) : string.Empty;
                dictionary[key] = val;
            }
            return dictionary;
        }
        // Dispose method for HttpManager
        public void Dispose()
        {
            _httpClient.Dispose();
            _httpHandler.Dispose();
            GC.SuppressFinalize(this);
        }
        // Finalizer for the HttpManager class.
        ~HttpManager()
        {
            Dispose();
        }
    }
    public class RequestResponse
    {
        public bool Successful { get; set; }
        public Dictionary<string, string> ResponseInfo { get; set; }
        public string UrlResponse { get; set; }
    }
}