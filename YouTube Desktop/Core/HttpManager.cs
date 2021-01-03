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
using System.Windows;
using System.IO;

namespace YouTube_Desktop.Core
{
    // HttpManager
    // Dev note: Some code will propbally break if youtube changes some requests and/or responses they provide. As 1-1-2021 it works!
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
                {0, "https://www.youtube.com/get_video_info?video_id={0}&el=detailpage&hl=en"}, // Second url for content not playable in embbeded (Vevo content etc)
                {1, "https://www.youtube.com/get_video_info?video_id={0}&hl=en"}, // if detailpage is not working. (less data tho.)
                {2, "https://www.youtube.com/get_video_info?video_id={0}&el=embedded&hl=en"} // The last option for some data.
            };
        private readonly string _embedRequest = "https://www.youtube.com/embed/{0}";
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
        public async Task<PlayerData> GetPlayerScriptDataAsync(string videoId)
        {
            PlayerData playerData = new PlayerData();
            string requestUrl = string.Format(_embedRequest, videoId);
            string response  = await GetResponseAsync(requestUrl);
            response = response.Replace("\\u0026", "&");
            response = response.Replace("u0026", "");
            string propYtSetconfig = "{'WEB_PLAYER_CONTEXT_CONFIGS':";
            string setConfigJson = null;
            if (response.Contains(propYtSetconfig))
            {
                setConfigJson = response.Substring(response.LastIndexOf(propYtSetconfig) + propYtSetconfig.Length);
                setConfigJson = setConfigJson.Substring(0, setConfigJson.LastIndexOf("}}});") + 2);
            }
            else
            {
                playerData.Success = false;
                playerData.ExceptionError = new Exception($"No valid {propYtSetconfig} found!");
            }
            try
            {
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(setConfigJson);
                playerData = JsonConvert.DeserializeObject<PlayerData>(jsonObj.GetValue("WEB_PLAYER_CONTEXT_CONFIG_ID_EMBEDDED_PLAYER").ToString());
                playerData.Success = true;
            }
            catch (Exception e)
            {
                playerData.Success = false;
                playerData.ExceptionError = e;
            }
            return playerData;
        }
        public async Task<string> GetCipherFunctionsAsync(PlayerData playerData)
        {
            if (playerData == null)
                return "";
            string jsSource = await GetResponseAsync(playerData.JSUrl);
            //jsResponse = jsResponse.Replace(");", ");\n");
            //Directory.CreateDirectory(@"D:\ApplicationTestWrites");
            //File.WriteAllText(@"D:\ApplicationTestWrites\TestWrite.js", jsResponse);
            // Find the name of the function that handles deciphering
            //string functionPattern = @"\.signature=([^\(]+)\(";
            //var funcName = Regex.Match(jsSource, functionPattern).Groups[1].Value;
            //if (funcName == null)
            //{
            //    functionPattern = "\\(\"signature\",([^\\(]+)\\(";
            //}
            string functionName = "=function(a){a=a.split(\"\");";
            string function = null;
            if (jsSource.Contains(functionName))
            {
                int startIndex = jsSource.LastIndexOf(functionName);
                string functionStart = jsSource.Substring(startIndex);
                function = functionStart.Substring(0, functionStart.IndexOf("};") + 2);
            }
            else
            {
                throw new Exception("Could not get the function!");
            }
            string[] subFunctions = function.Split(';');
            
            //var funcPattern = @"(?!h\.)" + Regex.Escape(funcName) + @"=function\(\w+\)\{(.*?)\}";
            //var funcBody = Regex.Match(jsResponse, funcPattern, RegexOptions.Singleline).Groups[1].Value;
            //if (string.IsNullOrEmpty(funcBody))
            //    throw new Exception("Could not find the signature decipherer function body");
            //var funcLines = funcBody.Split(';').ToArray();

            return "";
        }
        public async Task<string> GetResponseAsync(string requestUrl)
        {
            //using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            //{

            //}
            HttpRequestMessage httpRequestMsg = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUrl),
                Method = HttpMethod.Get
            };
            httpRequestMsg.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
            var response = await _httpClient.SendAsync(httpRequestMsg).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Get a video response.
        /// </summary>
        /// <param name="videoId">The video id.</param>
        /// <param name="decode">If the response needs to be decoded. (Is recommended to set to "true")</param>
        /// <param name="requestUrlIndex">For other request url.</param>
        /// <returns></returns>
        public async Task<RequestResponse> GetVideoResponseAsync(string videoId, bool decode = true, int requestUrlIndex = 0)
        {
            string requestUrl = string.Format(_videoRequestCalls[requestUrlIndex], videoId);
            string response = null;
            bool propsFound = false;
            int requestTrys = 0;
            while (!propsFound)
            {
                requestTrys++;
                response = await GetResponseAsync(requestUrl).ConfigureAwait(false);
                if (decode)
                {
                    response = WebUtility.UrlDecode(response);
                    response = response.Replace("\\u0026", "&");
                    response = response.Replace("u0026", "&");
                }

                //propsFound = (response.Contains("&signature=") && response.Contains("&player_response="));
                propsFound = (response.Contains("&player_response="));
                if (requestTrys >= 10) // To prevent infinite loop, after a total amount of trys we probally don't get is. (We don't want that... not that i have any xp with it.)
                    return new RequestResponse() { Successful = false, RequestUrl = requestUrl, ExceptionError = new Exception($"Could not get the required propeties from response, after {requestTrys} trys.") };
            }

            RequestResponse responseCheck = CheckResponse(response);
            responseCheck.RequestUrl = requestUrl;
            responseCheck.VideoId = videoId;
            return responseCheck;
        }
        public RequestResponse CheckResponse(string responseUrl)
        {
            RequestResponse reqRes = new RequestResponse();
            Dictionary<string, string> checkResponseDict = GetDictionaryFromResponse(responseUrl);
            if (checkResponseDict.TryGetValue("status", out string value))
                reqRes.Successful = (value != "fail");
            else
            {
                reqRes.Successful = false;
                reqRes.ExceptionError = new Exception($"Response {value}.\nReason: {(checkResponseDict.TryGetValue("reason", out string reason) ? reason : "Unknown (No reason provided.)")}");
            }
            // The siganture
            string signatureProp = "&signature=";
            if (responseUrl.Contains(signatureProp))
            {
                reqRes.Signature = responseUrl.Substring(responseUrl.LastIndexOf(signatureProp) + signatureProp.Length);
                reqRes.Signature = reqRes.Signature.Substring(0, reqRes.Signature.IndexOf('&'));
            }
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
        /// <summary>
        /// Well need it to be explained?
        /// </summary>
        public bool Successful { get; set; }
        /// <summary>
        /// If it errors her here the information about it.
        /// </summary>
        public Exception ExceptionError { get; set; }
        /// <summary>
        /// Response data.
        /// </summary>
        public Dictionary<string, string> ResponseInfo { get; set; }
        /// <summary>
        /// The response.
        /// </summary>
        public string UrlResponse { get; set; }
        /// <summary>
        /// The url where the request is made.
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// The video id.
        /// </summary>
        public string VideoId { get; set; }
        /// <summary>
        /// The signature
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// The json data.
        /// </summary>
        public YouTubeRequestJsonParseRaw rawJsonData { get; set; }
    }
}