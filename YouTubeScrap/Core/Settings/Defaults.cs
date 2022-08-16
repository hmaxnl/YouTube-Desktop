using System;
using System.IO;

namespace YouTubeScrap.Core.Settings
{
    /// <summary>
    /// Defaults class for loading the default settings/properties off the app.
    /// </summary>
    public static class Defaults
    {
        // Settings
        public static readonly string SettingsLocation = Path.Combine(Directory.GetCurrentDirectory(), SettingsFolder);
        public const string SettingsFolder = "Settings";
        public const string SettingsFile = "app_settings.json";
        
        // Caches/Storages/Logging
        private const string LogFile = "youtubed_.log";
        public static readonly string LogLocation = Path.Combine("logs", LogFile);
        
        public static readonly string StorageLocation = Path.Combine(Directory.GetCurrentDirectory(), StorageFolder);
        public const string StorageFolder = "Storage";
        public const string UserSubStorageFolder = "user_store";
        
        public static readonly string TempPath = Path.Combine(Path.GetTempPath(), "YTD_Temp");
        
        public const string UserSubCacheFolder = "user_cache";
        public const string ImageSubCacheFolder = "image_cache";
        
        public static string GetUserAgent()
        {
            // We use a firefox user agent because google changed that logins from apps/CEF will not work. Because 'security reasons'.
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
}