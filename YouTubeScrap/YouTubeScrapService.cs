using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YouTubeScrap.Core;
using YouTubeScrap.Models;

namespace YouTubeScrap
{
    /// <summary>
    /// The main YouTubeScrapService class for scrapping data from youtube.
    /// </summary>
    public class YouTubeScrapService : IDisposable
    {
        public static readonly string PROP_player_response = "player_response={";
        public static readonly string PROP_watch_next_response = "watch_next_response={";

        private readonly NetworkHandler _networkHandler;

        public YouTubeScrapService()
        {
            _networkHandler = new NetworkHandler();
        }
        public void Dispose()
        {
            _networkHandler.Dispose();
        }
        public void TestCall()
        {
            Task<HttpVideoResponse> taskResponse = Task.Run(async () => await _networkHandler.GetVideoResponseAsync("BD_guK9b64k"));
            HttpVideoResponse result = taskResponse.Result;
            result.Response = DecodeResponse(result.Response);
            VideoProperties videoProps = GetVideoProperties(result);
        }
        private VideoProperties GetVideoProperties(HttpVideoResponse videoResponse)
        {
            VideoProperties videoProps = new VideoProperties();
            string response = videoResponse.Response;
            if (videoResponse.ContainsPlayerResponse) videoProps.JsonPlayerResponse = PropertyToJson(response, PROP_player_response, out response);
            if (videoResponse.ContainsWatchNextResponse) videoProps.JsonWatchNextResponse = PropertyToJson(response, PROP_watch_next_response, out response);
            videoProps.PropertyDict = GetPropertyDict(response);
            return videoProps;
        }
        private JObject PropertyToJson(string propertyResponse, string property, out string cleanedResponse)
        {
            string[] propertyEnds = { "}}}}&", "\":{}}&", "\"}&" };
            string response = propertyResponse.Substring(propertyResponse.LastIndexOf(property) + property.Length - 1);
            for (int i = 0; i < propertyEnds.Length; i++)
            {
                if (response.Contains(propertyEnds[i]))
                {
                    response = response.Substring(0, response.LastIndexOf(propertyEnds[i]) + propertyEnds[i].Length - 1);
                    break;
                }
                else if (i == propertyEnds.Length - 1) response = Regex.Match(response, @"\{(.|\s)*\}").Groups[0].Value; // If this hits the json is changed on youtube's side, or the wrong data is passed. (Both can f*ck up deserialization.)
            }
            string valToRemove = property.Substring(0, property.Length - 1) + response;
            cleanedResponse = "&" + propertyResponse.Replace(valToRemove, string.Empty);
            response = WebUtility.UrlDecode(response);
            if (response.Contains("\\&")) response = response.Replace("\\&", "");
            try
            {
                return JsonConvert.DeserializeObject<JObject>(response);
            }
            catch (Exception)
            {
                throw; // Need to log the exception then yeet it.
            }
        }
        private Dictionary<string, string> GetPropertyDict(string response)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string[] splitted = response.Split('&');
            foreach (var property in splitted)
            {
                int equalPostCount = property.IndexOf('=');
                if (equalPostCount <= 0) continue;
                string key = property.Substring(0, equalPostCount);
                string value = equalPostCount < property.Length ? property.Substring(equalPostCount + 1) : string.Empty;
                properties[key] = value;
            }
            return properties;
        }
        private string DecodeResponse(string response)
        {
            response = WebUtility.UrlDecode(response);
            response = response.Replace("\\u0026", "&");
            return response.Replace("u0026", "&");
        }
    }
    public struct VideoProperties
    {
        public JObject JsonPlayerResponse { get; set; }
        public JObject JsonWatchNextResponse { get; set; }
        public Dictionary<string, string> PropertyDict { get; set; }
    }
}