using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Models;
using YouTubeScrap.Models.Media;
using YouTubeScrap.Models.Video.PlayerResponse;

namespace YouTubeScrap.Util
{
    // Convertaaah.
    public class JsonDeserialConverter : JsonConverter
    {
        private readonly string[] _subProps = { "simpleText", "baseUrl", "label" };
        private readonly Dictionary<string, string> _stringProps = new Dictionary<string, string>()
        {
            { "playerErrorMessageRenderer", "subreason"},
            { "baseUrl", string.Empty},
            { "simpleText", string.Empty },
            { "videoId", string.Empty },
            { "browseId", string.Empty },
            { "accessibility", "accessibilityData" }
        };
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                return null;
            object readerValObj = null;
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    readerValObj = JObject.Load(reader);
                    break;
                case JsonToken.StartArray:
                    readerValObj = JArray.Load(reader);
                    break;
                case JsonToken.String:
                    readerValObj = reader.Value.ToString();
                    break;
            }
            if (readerValObj == null)
                return null;
            // Switch for checking data and converters.
            switch (objectType.Name)
            {
                case nameof(DateTime):
                    if (long.TryParse(readerValObj as string, out long parsedVal))
                        return DateTime.FromFileTime(parsedVal);
                    if (DateTime.TryParse(readerValObj as string, out DateTime dtParse))
                        return dtParse;
                    return DateTime.MinValue;
                case nameof(String):
                    foreach (var item in _stringProps)
                    {
                        if ((readerValObj as JObject).ContainsKey(item.Key))
                            return GetValueFromStringConvert(readerValObj as JObject, item.Key, item.Value);
                    }
                    //if ((readerValObj as JObject).ContainsKey("playerErrorMessageRenderer"))
                    //    return GetValueFromStringConvert(readerValObj as JObject, "playerErrorMessageRenderer", "subreason");
                    //if ((readerValObj as JObject).ContainsKey("baseUrl"))
                    //    return GetValueFromStringConvert(readerValObj as JObject, "baseUrl", string.Empty);
                    return string.Empty;
                case nameof(VideoPlayabilityStatus):
                    if (Enum.TryParse<VideoPlayabilityStatus>(readerValObj as string, out VideoPlayabilityStatus result))
                        return result;
                    else return VideoPlayabilityStatus.ERROR;
                case nameof(MediaFormatRange):
                    return JsonConvert.DeserializeObject<MediaFormatRange>(readerValObj.ToString());
                case nameof(MediaFormatColorInfo):
                    return JsonConvert.DeserializeObject<MediaFormatColorInfo>(readerValObj.ToString());
                case nameof(AdaptiveFormat):
                    List<VideoMediaFormat> vmfList = new List<VideoMediaFormat>();
                    List<AudioMediaFormat> amfList = new List<AudioMediaFormat>();
                    List<CaptionMediaFormat> cmfList = new List<CaptionMediaFormat>();
                    foreach (var format in readerValObj as JArray)
                    {
                        if (format.Value<string>("mimeType").Contains("video/"))
                            vmfList.Add(JsonConvert.DeserializeObject<VideoMediaFormat>(format.ToString()));
                        if (format.Value<string>("mimeType").Contains("audio/"))
                            amfList.Add(JsonConvert.DeserializeObject<AudioMediaFormat>(format.ToString()));
                        if (format.Value<string>("mimeType").Contains("text/"))
                            cmfList.Add(JsonConvert.DeserializeObject<CaptionMediaFormat>(format.ToString()));
                    }
                    return new AdaptiveFormat() { VideoMedia = vmfList, AudioMedia = amfList, CaptionMedia = cmfList };
            }
            if (objectType == typeof(List<Thumbnail>))
            {
                JToken thumbnailToken = (readerValObj as JObject).GetValue("thumbnails");
                return JsonConvert.DeserializeObject<List<Thumbnail>>(thumbnailToken.ToString());
            }
            if (objectType == typeof(List<MixxedMediaFormat>))
            {
                return JsonConvert.DeserializeObject<List<MixxedMediaFormat>>((readerValObj as JArray).ToString());
            }
            if (objectType == typeof(List<EndScreenElement>))
            {
                JArray EndScreenElements = (readerValObj as JArray);
                List<EndScreenElement> endScreenElementsList = new List<EndScreenElement>();
                foreach (var item in EndScreenElements)
                {
                    JToken endscreenElement = (item as JObject).GetValue("endscreenElementRenderer");
                    endScreenElementsList.Add(JsonConvert.DeserializeObject<EndScreenElement>(endscreenElement.ToString()));
                }
                return endScreenElementsList;
            }
            return null;
        }
        private string GetValueFromStringConvert(JObject data, string value, string property)
        {
            JObject processObj;
            if (data.TryGetValue(value, out JToken tokenProp))
            {
                try
                {
                    processObj = tokenProp[property].ToObject<JObject>();
                }
                catch
                {
                    processObj = data;
                }
            }
            else
                processObj = data;
            JToken tokenSubProp = null;
            foreach (var subProp in _subProps)
            {
                if (processObj.TryGetValue(subProp, out tokenSubProp))
                    break;
            }
            return (tokenSubProp != null) ? tokenSubProp.ToString() : processObj.ToString();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}