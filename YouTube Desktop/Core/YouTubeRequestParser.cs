using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using YouTube_Desktop.Core.Models;
using YouTube_Desktop.Core.Models.Media;
using YouTube_Desktop.Core.Models.Video;

namespace YouTube_Desktop.Core
{
    // Need to make a parser for &watch_next_response= property
    /// <summary>
    /// PARSAAAAHR
    /// </summary>
    public class YouTubeRequestParser
    {
        HttpManager _httpManager;
        public YouTubeRequestParser()
        {
            _httpManager = new HttpManager();
        }
        public async Task<VideoInfoSnippet> GetVideoProperties(RequestResponse requestResponse)
        {
            if (requestResponse.rawJsonData.Equals(null) || !requestResponse.rawJsonData.Successfull)
                return new VideoInfoSnippet(requestResponse, null, null, null, null, null);
            
            PlayabilityStatus playabilityStatus = (PlayabilityStatus)GetPropertyJsonRaw(requestResponse.rawJsonData.ParsedJson, typeof(PlayabilityStatus));
            return new VideoInfoSnippet(requestResponse, await _httpManager.GetPlayerScriptDataAsync(requestResponse.VideoId), playabilityStatus,
                (playabilityStatus.Status == VideoStatus.OK) ? (StreamingData)GetPropertyJsonRaw(requestResponse.rawJsonData.ParsedJson, typeof(StreamingData)) : null,
                (playabilityStatus.Status == VideoStatus.OK || playabilityStatus.Status == VideoStatus.UNPLAYABLE) ? (VideoDetails)GetPropertyJsonRaw(requestResponse.rawJsonData.ParsedJson, typeof(VideoDetails)) : null,
                (playabilityStatus.Status == VideoStatus.OK || playabilityStatus.Status == VideoStatus.UNPLAYABLE) ? (Microformat)GetPropertyJsonRaw(requestResponse.rawJsonData.ParsedJson, typeof(Microformat)) : null);
        }
        private object GetPropertyJsonRaw(JObject jsonParsed, Type propertyClass)
        {
            object property = null;
            JToken token = null;
            // Kinda dirty tho, but it works.
            switch (propertyClass.Name)
            {
                case nameof(PlayabilityStatus):
                    token = jsonParsed.GetValue("playabilityStatus");
                    property = JsonConvert.DeserializeObject<PlayabilityStatus>(token.ToString());
                    break;
                case nameof(StreamingData):
                    token = jsonParsed.GetValue("streamingData");
                    property = JsonConvert.DeserializeObject<StreamingData>(token.ToString());
                    break;
                case nameof(VideoDetails):
                    token = jsonParsed.GetValue("videoDetails");
                    property = JsonConvert.DeserializeObject<VideoDetails>(token.ToString());
                    break;
                case nameof(Microformat):
                    JObject jsonObjectProp = JObject.Parse(jsonParsed.GetValue("microformat").ToString());
                    token = jsonObjectProp.GetValue("playerMicroformatRenderer");
                    property = JsonConvert.DeserializeObject<Microformat>(token.ToString());
                    break;
                default:
                    break;
            }
            return property;
        }
        /// <summary>
        /// This stuff is probably gonna break if youtube changes shit.
        /// </summary>
        /// <param name="ResponseData">The response returned from the http request. (If can decoded with url decode)</param>
        /// <returns></returns>
        internal Task<YouTubeRequestJsonParseRaw> GetJsonPlayerParseFromResponse(RequestResponse ResponseData)
        {
            YouTubeRequestJsonParseRaw result = new YouTubeRequestJsonParseRaw();
            result.RequestResponse = ResponseData;
            if (string.IsNullOrEmpty(ResponseData.UrlResponse))
            {
                result.Successfull = false;
                result.Exception = new Exception($"The response is not valid!");
                return Task.FromResult(result);
            }
            // The player_response that we need.
            string playerRes = "&player_response=";
            // If the first filter is not enough we will use this parameter for checking the end of the property we need.
            string checkForEnd = "\"}}}}&";
            // Get the player response parameter to end of string
            string playerResponse = ResponseData.UrlResponse.Substring(ResponseData.UrlResponse.LastIndexOf(playerRes) + playerRes.Length);
            // Checks for the second filter
            if (playerResponse.Contains(checkForEnd))
                // If true (Duh!) then get the index to the end of the property so we can deserialize it to valid json.
                // &player_response={VALID_JSON}&otherpropertythatwedontneed
                // checks if there is something after \"}}}}&, and removed it so we only get the player_response property.
                // And that we can use to deserialize it to valid json and then later deserialize it again to .Net objects.
                // Easy... right?
                playerResponse = playerResponse.Substring(0, playerResponse.LastIndexOf(checkForEnd));
            if (string.IsNullOrEmpty(playerResponse))
            {
                result.Successfull = false;
                result.Exception = new Exception($"Could not extract the {playerRes} property!");
                return Task.FromResult(result);
            }
            playerResponse = WebUtility.UrlDecode(playerResponse);
            if (playerResponse.Contains("\\&"))
            {
                playerResponse = playerResponse.Replace("\\&", "&");
            }
            // Get the valid JSON
            string playerResponseJson = Regex.Match(playerResponse, @"\{(.|\s)*\}").Groups[0].Value;
            try
            {
                result.ParsedJson = JsonConvert.DeserializeObject<JObject>(playerResponseJson);
                result.Successfull = true;
            }
            catch (Exception e)
            {
                result.Successfull = false;
                result.Exception = e;
            }
            return Task.FromResult(result);
        }
    }
    /// <summary>
    /// The struct to handle the raw data.
    /// </summary>
    public struct YouTubeRequestJsonParseRaw
    {
        /// <summary>
        /// The parsed json raw data.
        /// </summary>
        public JObject ParsedJson;
        /// <summary>
        /// If the parsing is successfull.
        /// </summary>
        public bool Successfull;
        /// <summary>
        /// The exception if the parsing has gone wrong.
        /// </summary>
        public Exception Exception;
        /// <summary>
        /// The request response data.
        /// </summary>
        public RequestResponse RequestResponse { get; set; }
    }
    
}