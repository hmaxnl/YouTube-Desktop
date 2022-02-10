using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Util.JSON
{
    public class JsonGuideConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                    JArray jArray = JArray.Load(reader);
                    List<object> guideSections = new List<object>();
                    foreach (var arrToken in jArray)
                    {
                        JObject tokenVal = arrToken.Value<JObject>();
                        // Sections
                        if (tokenVal != null && tokenVal.TryGetValue("guideSectionRenderer", out JToken gsr))
                        { guideSections.Add(JsonConvert.DeserializeObject<GuideSection>(gsr.ToString())); continue; }
                        if (tokenVal != null && tokenVal.TryGetValue("guideSubscriptionsSectionRenderer", out JToken gssr))
                        { guideSections.Add(JsonConvert.DeserializeObject<GuideSubscriptionSection>(gssr.ToString())); continue; }
                        if (tokenVal != null && tokenVal.TryGetValue("guideSigninPromoRenderer", out JToken gspr))
                        { guideSections.Add(JsonConvert.DeserializeObject<GuideSigninPromoRenderer>(gspr.ToString())); continue; }
                        // Entries
                        if (tokenVal != null && tokenVal.TryGetValue("guideEntryRenderer", out JToken ger))
                        { guideSections.Add(JsonConvert.DeserializeObject<GuideEntryRenderer>(ger.ToString())); continue; }
                        if (tokenVal != null && tokenVal.TryGetValue("guideCollapsibleEntryRenderer", out JToken gcer))
                        { guideSections.Add(JsonConvert.DeserializeObject<GuideCollapsibleEntryRenderer>(gcer.ToString())); continue; }
                        if (tokenVal != null && tokenVal.TryGetValue("guideCollapsibleSectionEntryRenderer", out JToken gcser))
                        { guideSections.Add(JsonConvert.DeserializeObject<GuideCollapsibleSectionEntryRenderer>(gcser.ToString())); continue; }
                        if (tokenVal != null && tokenVal.TryGetValue("guideDownloadsEntryRenderer", out JToken gder))
                        { guideSections.Add(JsonConvert.DeserializeObject<GuideDownloadsEntryRenderer>(gder.ToString())); continue; }
                    }
                    return guideSections;
                case JsonToken.StartObject:
                    JObject jObject = JObject.Load(reader);
                    break;
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}