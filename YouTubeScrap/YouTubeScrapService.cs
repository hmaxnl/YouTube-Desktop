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
using YouTubeScrap.Models.Video;
using YouTubeScrap.Models.Video.PlayerResponse;

namespace YouTubeScrap
{
    /// <summary>
    /// The main YouTubeScrapService class for scrapping data from youtube.
    /// </summary>
    public class YouTubeScrapService : IDisposable
    {
        internal static readonly string PROP_player_response = "player_response={";
        internal static readonly string PROP_watch_next_response = "watch_next_response={";

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
            
        }
        public VideoDataSnippet GetFullVideoSnippet(string videoId)
        {
            VideoDataSnippet videoSnippet = new VideoDataSnippet();
            if (string.IsNullOrEmpty(videoId))
                return videoSnippet;
            HttpVideoResponse result = GetVideoResponseFromId(videoId);
            VideoProperties videoProps = GetVideoProperties(result);
            videoSnippet.PlayabilityStatus = ConvertProperty<PlayabilityStatus>(videoProps.JsonPlayerResponse);
            videoSnippet.StreamingData = ConvertProperty<StreamingData>(videoProps.JsonPlayerResponse);
            videoSnippet.PlaybackTracking = ConvertProperty<PlaybackTracking>(videoProps.JsonPlayerResponse);
            videoSnippet.VideoCaptions = ConvertProperty<VideoCaptions>(videoProps.JsonPlayerResponse);
            videoSnippet.VideoDetails = ConvertProperty<VideoDetails>(videoProps.JsonPlayerResponse);
            videoSnippet.Microformat = ConvertProperty<Microformat>(videoProps.JsonPlayerResponse);
            videoSnippet.EndScreen = ConvertProperty<EndScreen>(videoProps.JsonPlayerResponse);
            videoSnippet.PaidContent = ConvertProperty<PaidContent>(videoProps.JsonPlayerResponse);
            return videoSnippet;
        }
        private HttpVideoResponse GetVideoResponseFromId(string videoId, bool decodeResponse = true)
        {
            Task<HttpVideoResponse> taskResponse = Task.Run(async () => await _networkHandler.GetVideoResponseAsync(videoId));
            HttpVideoResponse result = taskResponse.Result;
            if (decodeResponse)
                result.Response = DecodeResponse(result.Response);
            return result;
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
            if (ValidatePlayerVars(response, out string validated))
                response = validated;
            try
            {
                return JsonConvert.DeserializeObject<JObject>(response);
            }
            catch (Exception)
            {
                throw; // Need to log the exception then yeet it.
            }
        }
        private bool ValidatePlayerVars(string response, out string responseValidated)
        {
            if (!response.Contains("\"playerVars\":")) // Property not found so nothing to do here.
            {
                responseValidated = response;
                return false;
            }
            string endOfProp = "\",";
            string endOfJson = "\"}}}}&";
            int startIndex = response.IndexOf("\"playerVars\":");
            int lengthIndex;
            if (response.Contains(endOfJson))
            {
                var tempIndex = response.IndexOf(endOfJson);
                lengthIndex = (response.IndexOf(endOfProp, tempIndex) + endOfProp.Length) - startIndex;
            }
            else
                throw new Exception("Could not remove/extract the invalid json property.");
            //string teststring = response.Substring(startIndex, lengthIndex); // Can be used to get to the advert video player_response.
            responseValidated = response.Remove(startIndex, lengthIndex);
            return true;
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
        private T ConvertProperty<T>(JObject jsonFormat)
        {
            object property = null;
            if (jsonFormat == null)
                return (T)property;
            JToken token = null;
            switch (typeof(T).Name)
            {
                case nameof(PlayabilityStatus):
                    token = jsonFormat.GetValue("playabilityStatus");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<PlayabilityStatus>(token.ToString());
                    break;
                case nameof(StreamingData):
                    token = jsonFormat.GetValue("streamingData");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<StreamingData>(token.ToString());
                    break;
                case nameof(PlaybackTracking):
                    token = jsonFormat.GetValue("playbackTracking");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<PlaybackTracking>(token.ToString());
                    break;
                case nameof(VideoCaptions):
                    token = jsonFormat.GetValue("captions");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<VideoCaptions>(token.ToString());
                    break;
                case nameof(VideoDetails):
                    token = jsonFormat.GetValue("videoDetails");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<VideoDetails>(token.ToString());
                    break;
                case nameof(Microformat):
                    JObject objectMicroformat;
                    if (!ParseJObject(jsonFormat.GetValue("microformat"), out objectMicroformat))
                        break;
                    token = objectMicroformat.GetValue("playerMicroformatRenderer");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<Microformat>(token.ToString());
                    break;
                case nameof(EndScreen):
                    JObject objectEndscreen;
                    if (!ParseJObject(jsonFormat.GetValue("endscreen"), out objectEndscreen))
                        break;
                    token = objectEndscreen.GetValue("endscreenRenderer");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<EndScreen>(token.ToString());
                    break;
                case nameof(PaidContent):
                    JObject objectPaidContent;
                    if (!ParseJObject(jsonFormat.GetValue("paidContentOverlay"), out objectPaidContent))
                        break;
                    token = objectPaidContent.GetValue("paidContentOverlayRenderer");
                    if (token != null)
                        property = JsonConvert.DeserializeObject<PaidContent>(token.ToString());
                    break;
            }
            return (T)property;
        }
        private bool ParseJObject(object obj, out JObject jObject)
        {
            try
            {
                JObject objToParse = JObject.FromObject(obj);
                jObject = objToParse;
                return true;
            }
            catch (Exception)
            {
                jObject = null;
                return false;
            }
        }
    }
    public struct VideoProperties
    {
        public JObject JsonPlayerResponse { get; set; }
        public JObject JsonWatchNextResponse { get; set; }
        public Dictionary<string, string> PropertyDict { get; set; }
    }
}