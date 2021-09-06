using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace YouTubeScrap.Core
{
    public static class SettingsManager
    {
        public static AppSettings Settings => _settings;
        private static AppSettings _settings = DefaultSettings();
        
        private static string SettingsLocation => Path.Combine(Directory.GetCurrentDirectory(), "Settings");
        private static string SettingsFile => "settings.json";

        public static void LoadSettings()
        {
            Trace.WriteLine("Loading settings...");
            if (File.Exists(Path.Combine(SettingsLocation, SettingsFile)))
            {
                string settingsJSON = File.ReadAllText(Path.Combine(SettingsLocation, SettingsFile));
                _settings = JsonConvert.DeserializeObject<AppSettings>(settingsJSON);
            }
        }

        public static void SaveSettings()
        {
            Trace.WriteLine("Saving settings...");
            string settingsJSON = JsonConvert.SerializeObject(_settings);
            if (!Directory.Exists(SettingsLocation))
                Directory.CreateDirectory(SettingsLocation);
            File.WriteAllText(Path.Combine(SettingsLocation, SettingsFile), settingsJSON);
        }

        private static AppSettings DefaultSettings()
        {
            AppSettings appSettings = new AppSettings
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 Safari/537.36"
            };
            return appSettings;
        }
    }

    public struct AppSettings
    {
        [JsonProperty("userAgent")]
        public string UserAgent { get; set; }
    }
}