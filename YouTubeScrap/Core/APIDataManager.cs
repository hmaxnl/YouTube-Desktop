using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Handlers;

namespace YouTubeScrap.Core
{
    public static class ApiDataManager
    {
        public static InnertubeApiData InnertubeData => _innertubeData;
        private static InnertubeApiData _innertubeData;
        
        private static readonly Regex JsonRegex = new Regex(@"\{(?:[^\{\}]|(?<o>\{)|(?<-o>\}))+(?(o)(?!))\}");
        private static Task<HttpResponse> _responseTask;

        private static readonly string _clientState = "{\"CLIENT_CANARY_STATE\":";
        private static readonly string _responseContext = "{\"responseContext\":";
        
        // Get the innertube api details. Used for obtaining the innertube API key and more.
        public static JObject GetInnertubeData(YoutubeUser ytUser = null, bool getHomePage = true)
        {
            ApiRequest request = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Home);
            _responseTask = Task.Run(async () => await NetworkHandler.MakeApiRequestAsync(request, ytUser).ConfigureAwait(false));
            HttpResponse response = _responseTask.Result;
            return getHomePage ? ExtractJsonFromHtml(response.ResponseString) : null;
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
                if (match.Value.Contains(_responseContext))
                {
                    responseContext = JObject.Parse(match.Value);
                    if (partFound)
                        break;
                    partFound = true;
                }

                if (match.Value.Contains(_clientState))
                {
                    string searchValue = match.Value.Substring(match.Value.IndexOf(_clientState, StringComparison.Ordinal));
                    MatchCollection jsonMatch = JsonRegex.Matches(searchValue);
                    foreach (Match json in jsonMatch)
                    {
                        if (json.Value.Contains(_clientState))
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
                            continue;
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
}