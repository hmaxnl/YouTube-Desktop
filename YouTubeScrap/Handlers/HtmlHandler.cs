using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using YouTubeScrap.Core;
using YouTubeScrap.Core.ReverseEngineer.Cipher;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data.Innertube;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Handlers
{
    // Its not elegant but works... Until youtube changes the html.
    public static class HtmlHandler
    {
        private static readonly string ytInitialDataHTML = "var ytInitialData = ";
        private static readonly string ytInitialPlayerResponse = "var ytInitialPlayerResponse = ";
        private static readonly string ytcfgSetHtml = "window.ytplayer={};\nytcfg.set(";
        private static JObject playerProps = null;
        private static YoutubeUser _user;
        
        public static JObject ExtractJsonFromHtml(string html, HTMLExtractions extraction, YoutubeUser user = null)
        {
            JObject json = null;
            _user = user;
            switch (extraction)
            {
                case HTMLExtractions.InitialResponse:
                    if (Extract(html, ytInitialDataHTML, "};</script>", out string extractedResponseResult))
                        json = ParseJson(extractedResponseResult);
                    break;
                case HTMLExtractions.PlayerResponse:
                    if (Extract(html, ytInitialPlayerResponse, "};var", out string extractedResponsePlayerResult))
                    {
                        playerProps = ExtractJsonFromHtml(html, HTMLExtractions.Properties, _user);
                        json = ParseJson(extractedResponsePlayerResult, true);
                    }
                    break;
                case HTMLExtractions.Properties:
                    if (Extract(html, ytcfgSetHtml, "});", out string extractedPropertyResult))
                        json = ParseJson(extractedPropertyResult);
                    break;
                default:
                    Log.Information("Idk how this happened?");
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
                Log.Error(ex, "Could not get the required properties from the HTML document!");
                return false;
            }
            return true;
        }
        private static JObject ParseJson(string jsonString, bool withConverter = false)
        {
            JObject parsedJson = null;
            try
            {
                parsedJson = withConverter ? JsonConvert.DeserializeObject<JObject>(jsonString, new JsonDeserializeConverter(new CipherManager(_user, playerProps))) : JsonConvert.DeserializeObject<JObject>(jsonString);
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, "Exception while parsing JSON!");
                throw;
            }
            return parsedJson;
        }
        
        
        private const string ClientState = "{\"CLIENT_CANARY_STATE\":";
        private const string ResponseContext = "{\"responseContext\":";
        private static readonly Regex JsonRegex = new Regex(@"\{(?:[^\{\}]|(?<o>\{)|(?<-o>\}))+(?(o)(?!))\}");
        public static async Task<HtmlExtraction> ExtractFromHtmlAsync(string html)
        {
            if (html.IsNullEmpty())
                return new HtmlExtraction();
            Log.Information("Extracting HTML");
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(html);
            HtmlExtraction htmlExtract = new HtmlExtraction();
            await Task.Run(() =>
            {
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
                                        htmlExtract.ClientData =
                                            new Data.Innertube.ClientData(
                                                JObject.Parse(JsonRegex.Match(function).Value));
                                        break;
                                    case { } langDef when langDef.Contains("setMessage({"):
                                        htmlExtract.LanguageDefinitions =
                                            new LanguageDefinitions(JObject.Parse(JsonRegex.Match(function).Value));
                                        break;
                                }
                            }
                            break;
                        case { } responseContext when responseContext.Contains(ResponseContext):
                            JObject jsonMatch = JObject.Parse(JsonRegex.Match(responseContext).Value);
                            htmlExtract.Response = JsonConvert.DeserializeObject<JObject>(JsonRegex.Match(responseContext).Value, new JsonDeserializeConverter());
                            break;
                    }
                }
            });
            return htmlExtract;
        }
    }

    public struct HtmlExtraction
    {
        public JObject Response { get; set; }
        public ClientData ClientData { get; set; }
        public LanguageDefinitions LanguageDefinitions { get; set; }
    }
    public enum HTMLExtractions
    {
        InitialResponse,
        PlayerResponse,
        Properties
    }
}