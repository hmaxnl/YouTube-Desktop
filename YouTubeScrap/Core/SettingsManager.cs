using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YouTubeScrap.Core
{
    // Simple settings manager for saving and loading app settings in JSON format.
    public static class SettingsManager
    {
        public static AppSettings Settings
        {
            get
            {
                if (_settings == null)
                    LoadSettings();
                return _settings;
            }
        }
        private static AppSettings _settings;
        
        private static string SettingsLocation => Path.Combine(Directory.GetCurrentDirectory(), "Settings");
        private static string SettingsFile => "settings.json";

        public static void LoadSettings()
        {
            Trace.WriteLine("Loading settings...");
            if (File.Exists(Path.Combine(SettingsLocation, SettingsFile)))
                _settings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(Path.Combine(SettingsLocation, SettingsFile)));
            else
                _settings = DefaultSettings();
        }

        public static void SaveSettings()
        {
            Trace.WriteLine("Saving settings...");
            JObject settingsJObject = JObject.FromObject(Settings);
            if (!Directory.Exists(SettingsLocation))
                Directory.CreateDirectory(SettingsLocation);
            File.WriteAllText(Path.Combine(SettingsLocation, SettingsFile), settingsJObject.ToString());
        }

        private static AppSettings DefaultSettings()
        {
            AppSettings appSettings = new AppSettings
            {
                UserAgent = GetUserAgent(),
                UserStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "Cache", "UserStore")
            };
            return appSettings;
        }

        private static string GetUserAgent()
        {
            // We use a firefox user agent because google changed that logins from apps/CEF browsers will not work. In the name of security reasons.
            // Google want us to use OAuth and their API's but those suck!
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                    return "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:39.0) Gecko/20100101 Firefox/75.0"; // Windows 32-bit on 64-bit CPU - Firefox 75
                case PlatformID.MacOSX:
                    return "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.10; rv:75.0) Gecko/20100101 Firefox/75.0"; // MacOSX 10.10 Intel CPU - Firefox 75
                case PlatformID.Unix:
                default:
                    return "Mozilla/5.0 (X11; Linux ppc64le; rv:75.0) Gecko/20100101 Firefox/75.0"; // Linux powerPC - Firefox 75
            }
        }
    }

    public class AppSettings
    {
        [JsonProperty("UserAgent")]
        public string UserAgent { get; set; }
        [JsonProperty("UserStoragePath")]
        public string UserStoragePath { get; set; }
    }
}