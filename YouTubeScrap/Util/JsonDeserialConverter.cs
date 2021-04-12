using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Video;
using YouTubeScrap.Data.Media.Data;
using YouTubeScrap.Data.Media;
using YouTubeScrap.Data.Video.Data;

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
            { "accessibility", "accessibilityData" },
            { "runs", "text" }
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
                    {
                        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        DateTime dtResult = epoch.AddMilliseconds(parsedVal / 1000);
                        return dtResult;
                    }
                    break;
                case nameof(String):
                    foreach (var item in _stringProps)
                    {
                        if ((readerValObj as JObject).ContainsKey(item.Key))
                            return GetValueFromStringConvert(readerValObj as JObject, item.Key, item.Value);
                    }
                    return string.Empty;
                case nameof(VideoPlayabilityStatus):
                    if (Enum.TryParse<VideoPlayabilityStatus>(readerValObj as string, out VideoPlayabilityStatus result))
                        return result;
                    else return VideoPlayabilityStatus.ERROR;
                case nameof(MediaFormatRange):
                    return JsonConvert.DeserializeObject<MediaFormatRange>(readerValObj.ToString());
                case nameof(MediaFormatColorInfo):
                    return JsonConvert.DeserializeObject<MediaFormatColorInfo>(readerValObj.ToString());
                case nameof(ViewCounts):
                    return JsonConvert.DeserializeObject<ViewCounts>((readerValObj as JObject).GetValue("videoViewCountRenderer").ToString());
                case nameof(Sentiment):
                    return JsonConvert.DeserializeObject<Sentiment>((readerValObj as JObject).GetValue("sentimentBarRenderer").ToString());
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
            if (objectType == typeof(List<IContent>))
            {
                throw new NotImplementedException("Deserialization for the IContent list is not yet implemented!");
            }
            return null;
        }
        private object GetValueFromStringConvert(JObject data, string value, string property)
        {
            JObject processObj;
            if (data.TryGetValue(value, out JToken tokenProp))
            {
                if (value == "runs")
                {
                    JArray textArray = JArray.FromObject(data.GetValue("runs"));
                    List<string> listStr = new List<string>();
                    foreach (var text in textArray)
                        listStr.Add(JObject.FromObject(text).GetValue("text").ToString());
                    return listStr.ToArray();
                }
                else
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