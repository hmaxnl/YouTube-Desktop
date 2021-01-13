using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Models;
using YouTubeScrap.Models.Video;

namespace YouTubeScrap.Util
{
    // Convertaaah.
    public class JsonDeserialConverter : JsonConverter
    {
        private readonly string[] _subProps = { "simpleText" };
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
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
                    return GetValueFromStringConvert(readerValObj as JObject, "playerErrorMessageRenderer", "subreason");
                case nameof(VideoPlayabilityStatus):
                    if (Enum.TryParse<VideoPlayabilityStatus>(readerValObj as string, out VideoPlayabilityStatus result))
                        return result;
                    else return VideoPlayabilityStatus.ERROR;
            }
            if (objectType == typeof(List<Thumbnail>))
            {
                JToken thumbnailToken = (readerValObj as JObject).GetValue("thumbnails");
                return JsonConvert.DeserializeObject<List<Thumbnail>>(thumbnailToken.ToString());
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
            return (tokenSubProp != null) ? tokenSubProp.ToString() : string.Empty;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}