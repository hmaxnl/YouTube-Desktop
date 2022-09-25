using System.IO;
using Management;
using Newtonsoft.Json;

namespace YouTubeScrap.Core.Settings
{
    public class AppSettings
    {
        // Web
        [JsonProperty("UserAgent")]
        public string UserAgent { get; set; } = Registry.GetValue("App.network.userAgent")?.ToString();
        
        // Cache
        [JsonProperty("CachePath")]
        public string CachePath { get; set; } = Registry.GetValue("App.storage.directory")?.ToString();
        [JsonProperty("UserStorePath")]
        public string UserStorePath { get; set; } = Path.Combine(Defaults.StorageLocation, Defaults.UserSubStorageFolder);
        [JsonProperty("UserCachePath")]
        public string UserCachePath { get; set; } = Path.Combine(Defaults.TempPath, Defaults.UserSubCacheFolder);
        [JsonProperty("ImageCachePath")]
        public string ImageCachePath { get; set; } = Path.Combine(Defaults.TempPath, Defaults.ImageSubCacheFolder);
        
        // User
        [JsonProperty("DefaultUserId")]
        public string DefaultUserId { get; set; }
    }
}