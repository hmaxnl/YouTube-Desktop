using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTube_Desktop.Core.Models;
using YouTube_Desktop.Core.Models.Media;
using YouTube_Desktop.Core.Models.Video;

namespace YouTube_Desktop.Core
{
    // A simple json converter.
    public class JsonParserSerializationConverter : JsonConverter
    {
        public JsonParserSerializationConverter()
        {

        }
        public override bool CanConvert(Type objectType) // Not used.
        {
            throw new NotImplementedException();
        }

        private string[] _subProperties = { "simpleText" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject readObject = null; // For non array objects.
            JArray readArray = null; // For array objects.
            string readerValue = null;
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    readObject = JObject.Load(reader);
                    break;
                case JsonToken.StartArray:
                    readArray = JArray.Load(reader);
                    break;
                case JsonToken.String:
                    readerValue = reader.Value.ToString();
                    break;
            }
            
            // for checking structures and classes.
            switch (objectType.Name)
            {
                case nameof(MediaFormatRange):
                    return JsonConvert.DeserializeObject<MediaFormatRange>(readObject.ToString());
                case nameof(MediaFormatColorInfo):
                    return JsonConvert.DeserializeObject<MediaFormatColorInfo>(readObject.ToString());
                case nameof(DateTime):
                    long parsedValLong;
                    DateTime parsedValDateTime;
                    if (long.TryParse(readerValue, out parsedValLong))
                        return DateTime.FromFileTime(parsedValLong);
                    if (DateTime.TryParse(readerValue, out parsedValDateTime))
                        return parsedValDateTime;
                    return DateTime.Today;
                case nameof(String):
                    JToken tokenProp = null;
                    JObject processObject = null;
                    if (readObject.TryGetValue("playerErrorMessageRenderer", out JToken playerError))
                    {
                        try
                        {
                            processObject = playerError["subreason"].ToObject<JObject>();
                        }
                        catch
                        {
                            // No sub reason found.
                            processObject = readObject;
                        }
                    }
                    else
                        processObject = readObject;
                    foreach (var subProperty in _subProperties)
                    {
                        if (processObject.TryGetValue(subProperty, out tokenProp))
                            break;
                    }
                    return (tokenProp != null) ? tokenProp.ToString() : string.Empty;
                case nameof(AdaptiveFormat):
                    List<VideoMediaFormat> vmfList = new List<VideoMediaFormat>();
                    List<AudioMediaFormat> amfList = new List<AudioMediaFormat>();
                    foreach (var format in readArray)
                    {
                        if (format.Value<string>("mimeType").Contains("video/"))
                        {
                            vmfList.Add(JsonConvert.DeserializeObject<VideoMediaFormat>(format.ToString()));
                        }
                        if (format.Value<string>("mimeType").Contains("audio/"))
                        {
                            amfList.Add(JsonConvert.DeserializeObject<AudioMediaFormat>(format.ToString()));
                        }
                    }
                    return new AdaptiveFormat() { VideoMedia = vmfList, AudioMedia = amfList };
                case nameof(VideoStatus):
                    switch (readerValue)
                    {
                        case "OK":
                            return VideoStatus.OK;
                        case "UNPLAYABLE":
                            return VideoStatus.UNPLAYABLE;
                        case "ERROR":
                            return VideoStatus.ERROR;
                    }
                    break;
            }
            // For checking lists.
            if (objectType == typeof(List<Thumbnail>))
            {
                JToken thumbnailToken = readObject.GetValue("thumbnails");
                return JsonConvert.DeserializeObject<List<Thumbnail>>(thumbnailToken.ToString());
            }
            if (objectType == typeof(List<MixxedMediaFormat>))
            {
                return JsonConvert.DeserializeObject<List<MixxedMediaFormat>>(readArray.ToString());
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) // Not used.
        {
            throw new NotImplementedException();
        }
    }
}