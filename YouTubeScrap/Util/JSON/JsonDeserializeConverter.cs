using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using YouTubeScrap.Core;
using YouTubeScrap.Core.ReverseEngineer.Cipher;

namespace YouTubeScrap.Util.JSON
{
    /* Main JSON deserialize converter, used for converting JSON to usable format and for some adding,removing,manipulating of properties. */
    public class JsonDeserializeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        private readonly CipherManager _cipherManager;
        public JsonDeserializeConverter(CipherManager manager = null)
        {
            _cipherManager = manager;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Log.Information("Filtering JSON");
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    return JsonFilter(JObject.Load(reader));
                case JsonToken.StartArray:
                    JArray filteredArray = new JArray();
                    foreach (JToken token in JArray.Load(reader))
                        filteredArray.Add(FilterRelay(token));
                    return filteredArray;
                default:
                    return null;
            }
        }
        private JToken FilterRelay(JToken json)
        {
            if (json == null)
                return null;
            switch (json.Type)
            {
                case JTokenType.Object:
                    return JsonFilter(JObject.FromObject(json));
                case JTokenType.Array:
                    foreach (JToken item in JArray.FromObject(json))
                        return FilterRelay(item);
                    break;
            }
            return null;
        }
        private JObject JsonFilter(JObject json)
        {
            if (json == null)
                return null;
            List<JToken> descendantList = json.DescendantsAndSelf().ToList();
            for (int i = descendantList.Count() - 1; i >= 0; i--)// Reverse for loop, to get every property.
            {
                JToken itemToken = descendantList[i];
                if (!itemToken.HasValues)
                    continue;
                switch (itemToken.Type)
                {
                    case JTokenType.Property:
                        JProperty itemProperty = itemToken.ToObject<JProperty>();
                        string propertyValueName = itemProperty.Value.GetKey();
                        switch (itemProperty.Name)
                        {
                            case "expanderItem":
                            case "collapserItem":
                            case "headerEntry":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["guideEntryRenderer"]));
                                break;
                            case "commandMetadata":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["webCommandMetadata"]));
                                break;
                            case "hotkeyDialog":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["hotkeyDialogRenderer"]));
                                break;
                            case "topbar":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["desktopTopbarRenderer"]));
                                break;
                            case "searchbox":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["fusionSearchboxRenderer"]));
                                break;
                            case "richThumbnail":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["movingThumbnailRenderer"]));
                                break;
                            case "interstitial":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["consentBumpV2Renderer"]));
                                break;
                            case "languageList":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["dropdownRenderer"]));
                                break;
                            case "logo":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["topbarLogoRenderer"]));
                                break;
                            case "entryData":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["guideEntryData"]));
                                break;
                            case "microformat":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["playerMicroformatRenderer"]));
                                break;
                            case "miniplayer":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value["miniplayerRenderer"]));
                                break;
                            case "signatureCipher":
                                itemToken.AddBeforeSelf(new JProperty("url", _cipherManager?.DecipherAndBuildUrl(WebUtility.UrlDecode(itemProperty.Value.ToString()))));
                                break;
                            case "formats":
                                itemToken.AddAfterSelf(new JProperty("mixxedFormats", itemProperty.Value));
                                itemToken.Remove();
                                break;
                            case "lastModified":
                                itemToken.AddAfterSelf(new JProperty("lastModifiedDT", new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(double.TryParse(itemProperty.Value.ToString(), out double milliseconds) ? milliseconds / 1000 : 0)));
                                itemToken.Replace(new JProperty("lastModifiedMS", itemProperty.Value));
                                break;
                            case "expiresInSeconds":
                                itemToken.AddAfterSelf(new JProperty("expires", DateTime.Now.AddSeconds(double.TryParse(itemProperty.Value.ToString(), out double expiresSeconds) ? expiresSeconds : 0)));
                                break;
                            case "approxDurationMs":
                                itemToken.AddAfterSelf(new JProperty("approxDuration", TimeSpan.FromMilliseconds(double.TryParse(itemProperty.Value.ToString(), out double approxDurationMs) ? approxDurationMs : 0)));
                                break;
                            case "contentLength":
                                itemToken.AddAfterSelf(new JProperty("fileSize", SuffixUtil.SizeSuffix(long.TryParse(itemProperty.Value.ToString(), out long contentLengthBytes) ? contentLengthBytes : 0, 2)));
                                itemToken.Replace(new JProperty("contentLengthBytes", itemProperty.Value));
                                break;
                            case "qualityLabel":
                                switch (itemProperty.Value.ToString())
                                {
                                    case "4320p":
                                        itemToken.AddAfterSelf(new JProperty("qualityBadge", "8K"));
                                        break;
                                    case "2160p":
                                        itemToken.AddAfterSelf(new JProperty("qualityBadge", "4K"));
                                        break;
                                    case "1440p":
                                    case "1080p":
                                    case "720p": // 720p is not set as HD by youtube, but for now it will.
                                        itemToken.AddAfterSelf(new JProperty("qualityBadge", "HD"));
                                        break;
                                    case "480p":
                                        itemToken.AddAfterSelf(new JProperty("qualityBadge", "ED"));
                                        break;
                                    case "240p":
                                        itemToken.AddAfterSelf(new JProperty("qualityBadge", "SD"));
                                        break;
                                }
                                break;
                            case "adaptiveFormats":
                                JArray videoFormats = new JArray();
                                JArray audioFormats = new JArray();
                                foreach (JToken format in itemProperty.Value as JArray)
                                {
                                    if (format.TryGetToken("mimeType", out JToken mimeType))
                                    {
                                        if (mimeType.ToString().ContainsKey("video/"))
                                            videoFormats.Add(format);
                                        if (mimeType.ToString().ContainsKey("audio/"))
                                            audioFormats.Add(format);
                                    }
                                }
                                itemToken.AddAfterSelf(new JProperty("videoFormats", videoFormats));
                                itemToken.AddAfterSelf(new JProperty("audioFormats", audioFormats));
                                itemToken.Remove();
                                break;
                        }
                        switch (propertyValueName)
                        {
                            case "iconType":
                                itemToken.Replace(new JProperty(itemProperty.Name, itemProperty.Value[propertyValueName]));
                                break;
                            case "thumbnails":
                                if (!itemProperty.Name.ContainsKey("renderer"))
                                    itemToken.Replace(new JProperty(itemProperty.Name.ContainsKey("thumbnail") ? propertyValueName : itemProperty.Name, itemProperty.Value[propertyValueName]));
                                break;
                        }
                        break;
                    case JTokenType.Object:
                        string tokenName = itemToken.GetKey();
                        switch (tokenName)
                        {
                            case "hotkeyDialogSectionRenderer":
                            case "hotkeyDialogSectionOptionRenderer":
                            case "dropdownItemRenderer":
                            case "childVideoRenderer":
                            case "buttonRenderer":
                                itemToken.Replace(itemToken[tokenName]);
                                break;
                        }
                        break;
                    default:
                        continue;
                }
            }
            return json;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}