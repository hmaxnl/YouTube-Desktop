using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Handlers;

namespace YouTubeScrap
{
    /// <summary>
    /// The main communication API for youtube scraping!
    /// </summary>
    public static class YouTubeService
    {
        /*public YouTubeService(YoutubeUser ytUser)
        {
            Task.Run(() =>
            {
                _ytResponse = DataManager.GetData(ytUser);
            });
        }*/
        public static void TestRequester(YoutubeUser youtubeUser)
        {
            //ApiRequest apiRequest;
            //HttpResponse httpResponse;
            //apiRequest = YoutubeApiManager.GetApiRequest(ApiRequestType.Home);
            //apiRequest = YoutubeApiManager.GetApiRequest(ApiRequestType.AccountGet);
            //apiRequest = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Video, null, null, "h3fUgOKFMNU");// ZK3U92URi_c
            //Task<HttpResponse> requestTask = Task.Run(async () => await NetworkHandler.MakeRequestAsync(apiRequest, youtubeUser).ConfigureAwait(false));
            //httpResponse = requestTask.Result;
            //JObject propertiesJson = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.Properties);
            //JObject playerResponseJson = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.PlayerResponse);
            //JObject responseJson = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.InitialResponse);
            //var rc = JsonConvert.DeserializeObject<VideoDataSnippet>(response.ToString());
            return;
        }
        public static ResponseMetadata GetHome(YoutubeUser youtubeUser, string token = null)
        {
            return null;
        }
        public static ResponseMetadata GetSearch(string query, YoutubeUser youtubeUser, string token = null)
        {
            return null;
        }
        public static ResponseMetadata GetVideo(string videoId, YoutubeUser youtubeUser)
        {
            //ApiRequest apiRequest;
            //HttpResponse httpResponse;
            //apiRequest = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Video, null, null, videoId);
            //Task<HttpResponse> requestTask = Task.Run(async () => await NetworkHandler.MakeApiRequestAsync(apiRequest, youtubeUser).ConfigureAwait(false));
            //httpResponse = requestTask.Result;

            //JObject propertiesJSON = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.Properties);
            //JObject playerResponseJSON = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.PlayerResponse);
            //JObject initialResponseJSON = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.InitialResponse);

            //VideoDataSnippet videoData = JsonConvert.DeserializeObject<VideoDataSnippet>(playerResponseJSON.ToString());
            return null;
        }
    }
}