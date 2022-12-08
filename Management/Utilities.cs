using System;

namespace Management
{
    public static class Utilities
    {
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