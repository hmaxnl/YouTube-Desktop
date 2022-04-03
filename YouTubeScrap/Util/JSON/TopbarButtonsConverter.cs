using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeScrap.Util.JSON
{
    public class TopbarButtonsConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<object> btnList = new List<object>();
            try
            {
                switch (reader.TokenType)
                {
                    case JsonToken.StartArray:
                        JArray arr = JArray.Load(reader);
                        foreach (var btnToken in arr)
                        {
                            if (btnToken.TryGetToken("topbarMenuButtonRenderer", out JToken menuBtnToken))
                            {
                                MenuButtonRenderer mBtnRenderer = menuBtnToken.ToObject<MenuButtonRenderer>();
                                btnList.Add(mBtnRenderer);
                            }
                            else // If there is no menu button it will probably a noraml button.
                            {
                                ButtonRenderer btnRenderer = btnToken.ToObject<ButtonRenderer>();
                                btnList.Add(btnRenderer);
                            }
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Error while converting topbar buttons to objects!");
            }
            return btnList;
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