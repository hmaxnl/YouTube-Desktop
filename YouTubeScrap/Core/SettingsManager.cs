using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using YouTubeScrap.Core.Settings;

namespace YouTubeScrap.Core
{
    // Simple settings manager for saving and loading app settings in JSON format.
    public static class SettingsManager
    {
        static SettingsManager() => LoadSettings();
        public static AppSettings Settings
        {
            get
            {
                if (_settings == null)
                    LoadSettings();
                return _settings;
            }
            set => _settings = value;
        }
        private static AppSettings _settings;

        public static void LoadSettings()
        {
            Log.Information("Loading settings...");
            try
            {
                if (File.Exists(Path.Combine(Defaults.SettingsLocation, Defaults.SettingsFile)))
                {
                    using StreamReader reader =
                        new StreamReader(Path.Combine(Defaults.SettingsLocation, Defaults.SettingsFile));
                    _settings = JsonConvert.DeserializeObject<AppSettings>(reader.ReadToEnd());
                }
                else
                    _settings = new AppSettings();
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not load the configuration from disk!");
            }
        }

        public static void SaveSettings()
        {
            Log.Information("Saving settings...");
            try
            {
                if (!Directory.Exists(Defaults.SettingsLocation))
                    Directory.CreateDirectory(Defaults.SettingsLocation);
                File.WriteAllText(Path.Combine(Defaults.SettingsLocation, Defaults.SettingsFile), JObject.FromObject(Settings).ToString());
            }
            catch (Exception e)
            {
                Log.Error(e, "Error writing settings to disk!");
            }
        }
    }
}