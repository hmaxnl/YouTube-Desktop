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
        public static NetworkData NetworkData;
    }
    public struct NetworkData
    {
        public string Origin => "https://www.youtube.com";
        public string UserAgent => SettingsManager.Settings.UserAgent;
    }
}