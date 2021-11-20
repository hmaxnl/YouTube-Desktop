using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core;
using YouTubeScrap.Core.ReverseEngineer.Cipher;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Handlers
{
    // Its not elegant but works... Until youtube changes the html.
    //TODO: Check later if this is longer needed else delete it!
    public static class HtmlHandler
    {
        private static readonly string ytInitialDataHTML = "var ytInitialData = ";
        private static readonly string ytInitialPlayerResponse = "var ytInitialPlayerResponse = ";
        private static readonly string ytcfgSetHtml = "window.ytplayer={};\nytcfg.set(";
        private static JObject playerProps = null;
        public static JObject ExtractJsonFromHtml(string html, HTMLExtractions extraction)
        {
            JObject json = null;
            switch (extraction)
            {
                case HTMLExtractions.InitialResponse:
                    if (Extract(html, ytInitialDataHTML, "};</script>", out string extractedResponseResult))
                        json = ParseJson(extractedResponseResult);
                    break;
                case HTMLExtractions.PlayerResponse:
                    if (Extract(html, ytInitialPlayerResponse, "};var", out string extractedResponsePlayerResult))
                    {
                        playerProps = ExtractJsonFromHtml(html, HTMLExtractions.Properties);
                        json = ParseJson(extractedResponsePlayerResult, true);
                    }
                    break;
                case HTMLExtractions.Properties:
                    if (Extract(html, ytcfgSetHtml, "});", out string extractedPropertyResult))
                        json = ParseJson(extractedPropertyResult);
                    break;
                default:
                    Trace.WriteLine("Idk how this happend?");
                    break;
            }
            return json;
        }
        private static bool Extract(string html, string property, string endNotation, out string result)
        {
            result = string.Empty;
            //string htmlDecoded = WebUtility.UrlDecode(html);
            string htmlDecoded = html;
            int startIndex = htmlDecoded.LastIndexOf(property) + property.Length;
            int endIndex = htmlDecoded.IndexOf(endNotation, startIndex) + 1;
            int indexLength = endIndex - startIndex;
            try
            {
                result = htmlDecoded.Substring(startIndex, indexLength);
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine($"Could not get the specified properties from the HTML document! Message: {ex.Message}");
                return false;
            }
            return true;
        }
        private static JObject ParseJson(string jsonString, bool withConverter = false)
        {
            JObject parsedJson = null;
            try
            {
                //parsedJson = withConverter ? JsonConvert.DeserializeObject<JObject>(jsonString, new JsonDeserializeConverter(new CipherManager(playerProps))) : JsonConvert.DeserializeObject<JObject>(jsonString);
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine($"Exception while parsing JSON. Message: {ex.Message}");
            }
            return parsedJson;
        }
        
        
        private const string ClientState = "{\"CLIENT_CANARY_STATE\":";
        private const string ResponseContext = "{\"responseContext\":";
        private static readonly Regex JsonRegex = new Regex(@"\{(?:[^\{\}]|(?<o>\{)|(?<-o>\}))+(?(o)(?!))\}");
        public static HtmlExtraction ExtractFromHtml(string html)
        {
            if (html.IsNullEmpty())
                return new HtmlExtraction();
            
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(html);
            HtmlExtraction htmlExtract = new HtmlExtraction();
            ClientData clientData = new ClientData();
            foreach (IHtmlScriptElement scriptElement in doc.Scripts)
            {
                switch (scriptElement.InnerHtml)
                {
                    case { } clientState when clientState.Contains(ClientState):
                        string[] functs = clientState.Split(new[] { "};" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string function in functs)
                        {
                            switch (function)
                            {
                                case { } clientStateCfg when clientStateCfg.Contains("ytcfg.set({"):
                                    clientData.ClientState = JObject.Parse(JsonRegex.Match(function).Value);
                                    break;
                                case { } langDef when langDef.Contains("setMessage({"):
                                    clientData.LanguageDefinitions = JObject.Parse(JsonRegex.Match(function).Value);
                                    break;
                            }
                        }
                        break;
                    case { } responseContext when responseContext.Contains(ResponseContext):
                        htmlExtract.Response = JsonConvert.DeserializeObject<JObject>(JsonRegex.Match(responseContext).Value, new JsonDeserializeConverter());
                        break;
                }
            }
            htmlExtract.ClientData = clientData;
            return htmlExtract;
        }
    }

    public struct HtmlExtraction
    {
        public JObject Response { get; set; }
        public ClientData ClientData { get; set; }
    }
    public enum HTMLExtractions
    {
        InitialResponse,
        PlayerResponse,
        Properties
    }
}