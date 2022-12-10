using System.Collections.Generic;
using System.Net.Http;

namespace YouTubeScrap.Util
{
    public static class UrlUtility
    {
        // Google urls
        private readonly static string googleLoginUrl = "https://accounts.google.com/signin/v2/identifier?service=youtube&uilel=3&passive=true&continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Faction_handle_signin%3Dtrue%26app%3Ddesktop%26hl%3Dnl%26next%3Dhttps%253A%252F%252Fwww.youtube.com%252F&hl=nl&ec=65620&flowName=GlifWebSignIn&flowEntry=ServiceLogin";
        // Youtube urls
        private readonly static string youtubeHomeUrl = "https://www.youtube.com/"; // Args: ?hl=en&persist_hl=1
        private readonly static string youtubeSearchUrl = "https://www.youtube.com/results?search_query={0}";
        private readonly static string videoUrl = "https://www.youtube.com/watch?v={0}"; // Args: cc_load_policy=1&cc_lang_pref=en
        private readonly static string channelUrl = "https://www.youtube.com/channel/{0}";
        private readonly static string playlistUrl = "https://www.youtube.com/playlist?list={0}";
        //private readonly static string getVideoInfoUrl = "https://www.youtube.com/get_video_info?video_id={0}&el=detailpage&hl=en";
        // Official API urls
        //private readonly static string searchYoutubeApiUrl = "https://apis.google.com/youtubei/v1/search?search_query={0}&key={1}";
        //private readonly static string searchYoutubeApiV3Url = "https://www.googleapis.com/youtube/v3/search?part=snippet&q={0}&maxResults={1}&key={2}";
        //private readonly static string playlistApiUrl = "https://youtube.com/list_ajax?style=json&action_get_list=1&list={0}&index={1}&hl=en";
        // Ajax urls
        //private readonly static string videoCommentAjaxUrl = "https://www.youtube.com/comment_service_ajax?action_get_comments=1&pbj=1&ctoken={continuation}&continuation={continuation}&itct={continuation}";
        //private readonly static string videoSearchAjaxUrl = "https://youtube.com/search_ajax?style=json&search_query={0}&page={1}&hl=en";

        public static UrlData GetRequestUrl(RequestUrl requestUrl, UrlBuildData urlBuildData = null)
        {
            UrlData urlData = new UrlData();
            switch (requestUrl)
            {
                case RequestUrl.YoutubeHome:
                    urlData.Url = youtubeHomeUrl;
                    break;
                case RequestUrl.YoutubeSearch:
                    urlData.Url = youtubeSearchUrl;
                    break;
                case RequestUrl.YoutubeVideo:
                    urlData.Url = videoUrl;
                    break;
                case RequestUrl.YoutubePlaylist:
                    urlData.Url = playlistUrl;
                    break;
                case RequestUrl.YoutubeChannel:
                    urlData.Url = channelUrl;
                    break;
                case RequestUrl.GoogleLogin:
                    urlData.Url = googleLoginUrl;
                    break;
                case RequestUrl.Custom:
                    break;
            }
            return urlData;
        }
    }
    public enum RequestUrl
    {
        YoutubeHome,
        YoutubeSearch,
        YoutubeVideo,
        YoutubePlaylist,
        YoutubeChannel,
        GoogleLogin,
        Custom
    }
    public class UrlBuildData
    {
        public string Query { get; set; }
        public string OverrideUrl { get; set; }
        public string[] Argruments { get; set; }
        public string[] Parameters { get; set; }
        public int Index { get; set; }
    }
    public struct UrlData
    {
        public string Url { get; set; }
        public Dictionary<string, string> RequiredHeaders { get; set; }
        public string ContentType { get; set; }
        public HttpMethod Method { get; set; }
    }
}