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
        public static InnertubeAPIData InnertubeData => _innertubeData;
        private static InnertubeAPIData _innertubeData;
        
        private static Regex JsonRegex = new Regex(@"\{(?:[^\{\}]|(?<o>\{)|(?<-o>\}))+(?(o)(?!))\}");
        private static Task<HttpResponse> _responseTask;

        private static readonly string _clientState = "{\"CLIENT_CANARY_STATE\":";
        private static readonly string _responseContext = "{\"responseContext\":";
        
        // Get the innertube api details. Used for obtaining the innertube API key and version.
        public static void GetInnertubeData(YoutubeUser ytUser = null)
        {
            ApiRequest request = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Home);
            _responseTask = Task.Run(async () => await NetworkHandler.MakeRequestAsync(request, ytUser).ConfigureAwait(false));
            HttpResponse response = _responseTask.Result;
            JObject responseContext = ExtractJsonFromHtml(response.ResponseString);
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
                            _innertubeData.ClientStateJSON = JObject.Parse(json.Value);
                            continue;
                        }
                        try
                        {
                            _innertubeData.LanguageDefinitionsJSON = JObject.Parse(json.Value);
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

    public struct InnertubeAPIData
    {
        public JObject ClientStateJSON { get; set; }
        public JObject LanguageDefinitionsJSON { get; set; }
        public string ApiKey => ClientStateJSON.GetValue("INNERTUBE_API_KEY")?.ToString();
    }
}