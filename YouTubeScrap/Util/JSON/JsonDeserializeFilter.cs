using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YouTubeScrap.Util.JSON
{
    public class JsonDeserializeFilter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            Trace.WriteLine("Filtering JSON property...");
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    JObject objJson = JObject.Load(reader);
                    return null;
                case JsonToken.StartArray:
                    JArray arrJson = JArray.Load(reader);
                    return null;
                default:
                    return null;
            }
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}