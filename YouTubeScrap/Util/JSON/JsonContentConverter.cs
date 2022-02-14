using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Util.JSON
{
    public class JsonContentConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                    JArray jArray = JArray.Load(reader);
                    List<ContentRender> cRenderList = JsonConvert.DeserializeObject<List<ContentRender>>(jArray.ToString());
                    if (cRenderList == null) return null;
                    List<object> contentsList = new List<object>();
                    foreach (var cRender in cRenderList)
                    {
                        if (cRender.ContinuationItem != null)
                        { contentsList.Add(cRender.ContinuationItem); continue; }
                        if (cRender.ChipCloudChip != null)
                        { contentsList.Add(cRender.ChipCloudChip); continue; }

                        if (cRender.RichItem != null)
                        { contentsList.Add(cRender.RichItem.Content); continue; }
                        if (cRender.RichSection != null)
                        { contentsList.Add(cRender.RichSection.Content); continue; }
                    }
                    return contentsList;
                case JsonToken.StartObject:
                    JObject jObject = JObject.Load(reader);
                    if (reader.Path.Contains("richItem"))
                    {
                        RichItemContent ric = JsonConvert.DeserializeObject<RichItemContent>(jObject.ToString());
                        if (ric?.RichVideoContent != null)
                            return ric.RichVideoContent;
                        if (ric?.RadioRenderer != null)
                            return ric.RadioRenderer;
                        if (ric?.DisplayAdRenderer != null)
                            return ric.DisplayAdRenderer;
                    }
                    if (reader.Path.Contains("richSection"))
                    {
                        RichSectionContent rsc = JsonConvert.DeserializeObject<RichSectionContent>(jObject.ToString());
                        if (rsc?.InlineSurveyRenderer != null)
                            return rsc.InlineSurveyRenderer;
                        if (rsc?.PromotedItemRenderer != null)
                            return rsc.PromotedItemRenderer;
                        if (rsc?.RichShelfRenderer != null)
                            return rsc.RichShelfRenderer;
                    }
                    break;
            }
            return null;
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