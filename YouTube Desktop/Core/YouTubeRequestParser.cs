using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using YouTube_Desktop.Core.Models;
using YouTube_Desktop.Core.Models.Media;
using YouTube_Desktop.Core.Models.Video;

namespace YouTube_Desktop.Core
{
    /// <summary>
    /// PARSAAAAHR
    /// </summary>
    public class YouTubeRequestParser
    {
        public Task<VideoInfoSnippet> GetVideoProperties(YouTubeRequestJsonParseRaw ytrJsonPR)
        {
            if (ytrJsonPR.Equals(null) || !ytrJsonPR.Successfull)
                return Task.FromResult(new VideoInfoSnippet());
            PlayabilityStatus playabilityStatus = (PlayabilityStatus)GetPropertyJsonRaw(ytrJsonPR.ParsedJson, typeof(PlayabilityStatus));
            return Task.FromResult(new VideoInfoSnippet(playabilityStatus,
                (playabilityStatus.Status == VideoStatus.OK) ? (StreamingData)GetPropertyJsonRaw(ytrJsonPR.ParsedJson, typeof(StreamingData)) : null,
                (playabilityStatus.Status == VideoStatus.OK || playabilityStatus.Status == VideoStatus.UNPLAYABLE) ? (VideoDetails)GetPropertyJsonRaw(ytrJsonPR.ParsedJson, typeof(VideoDetails)) : null,
                (playabilityStatus.Status == VideoStatus.OK || playabilityStatus.Status == VideoStatus.UNPLAYABLE) ? (Microformat)GetPropertyJsonRaw(ytrJsonPR.ParsedJson, typeof(Microformat)) : null));
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
        internal Task<YouTubeRequestJsonParseRaw> GetJsonPlayerParseFromResponse(string decodedResponse) // NEED RESPONSE CHECK
        {
            YouTubeRequestJsonParseRaw result = new YouTubeRequestJsonParseRaw();
            if (string.IsNullOrEmpty(decodedResponse))
            {
                result.Successfull = false;
                result.Exception = new Exception($"The response is not valid!");
                return Task.FromResult(result);
            }
            string playerRes = "&player_response=";
            // Get the player response parameter to end of string
            string playerResponse = decodedResponse.Substring(decodedResponse.LastIndexOf(playerRes) + playerRes.Length);
            if (string.IsNullOrEmpty(playerResponse))
            {
                result.Successfull = false;
                result.Exception = new Exception($"Could not extract the {playerRes} property!");
                return Task.FromResult(result);
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
    }
    
}