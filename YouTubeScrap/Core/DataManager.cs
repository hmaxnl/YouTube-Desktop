using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core.ReverseEngineer.Cipher;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Handlers;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Core
{
    // A class for global data to be used over the whole app/library.
    public static class DataManager
    {
        public static InnertubeApiData InnertubeData => _innertubeData;
        private static InnertubeApiData _innertubeData;
        public static NetworkData NetworkData;
        
        private static readonly Regex JsonRegex = new Regex(@"\{(?:[^\{\}]|(?<o>\{)|(?<-o>\}))+(?(o)(?!))\}");
        private static Task<HttpResponse> _responseTask;

        private const string ClientState = "{\"CLIENT_CANARY_STATE\":";
        private const string ResponseContext = "{\"responseContext\":";

        public static JObject GetData(YoutubeUser ytUser)
        {
            ApiRequest request = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Home);
            HttpResponse response = ytUser.NetworkHandler.MakeApiRequestAsync(request, true).Result;
            return ExtractJsonFromHtml(response.ResponseString);
        }
        
        private static JObject ExtractJsonFromHtml(string htmlData)
        {
            if (htmlData.IsNullEmpty())
                return null;
            MatchCollection regexMatch = JsonRegex.Matches(htmlData);
            bool partFound = false;
            JObject responseContext = null;
            foreach (Match match in regexMatch)
            {
                if (match.Value.Contains(ResponseContext))
                {
                    responseContext =
                        JsonConvert.DeserializeObject<JObject>(match.Value, new JsonDeserializeConverter());
                    if (partFound)
                        break;
                    partFound = true;
                }

                if (match.Value.Contains(ClientState))
                {
                    string searchValue = match.Value.Substring(match.Value.IndexOf(ClientState, StringComparison.Ordinal));
                    MatchCollection jsonMatch = JsonRegex.Matches(searchValue);
                    foreach (Match json in jsonMatch)
                    {
                        if (json.Value.Contains(ClientState))
                        {
                            _innertubeData.ClientStateJson = JObject.Parse(json.Value);
                            continue;
                        }
                        try
                        {
                            _innertubeData.LanguageDefinitionsJson = JObject.Parse(json.Value);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    if (partFound)
                        break;
                    partFound = true;
                }
            }
            return responseContext;
        }
    }

    public struct InnertubeApiData
    {
        public JObject ClientStateJson { get; set; }
        public JObject LanguageDefinitionsJson { get; set; }
        public string ApiKey => ClientStateJson.GetValue("INNERTUBE_API_KEY")?.ToString();
        public string LoginUrl => ClientStateJson.GetValue("SIGNIN_URL")?.ToString();
    }

    public struct NetworkData
    {
        public string Origin => "https://www.youtube.com";
        public string UserAgent => SettingsManager.Settings.UserAgent;
    }
}