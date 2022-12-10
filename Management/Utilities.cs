using System;

namespace Management
{
    public static class Utilities
    {
        public static string GetUserAgent()
        {
            // We use a firefox user agent because google changed that logins from apps/CEF will not work. Because of 'security reasons'.
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
                case PlatformID.Other:
                default:
                    return "Mozilla/5.0 (X11; Linux ppc64le; rv:75.0) Gecko/20100101 Firefox/75.0"; // Linux powerPC - Firefox 75
            }
        }
        
        private static readonly string[] SizeSuffixes =
            { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        // Stolen from stackoverflow!
        // https://stackoverflow.com/a/14488941/9948300
        /// <summary>
        /// Convert bytes to the correct size suffix.
        /// </summary>
        /// <param name="bytes">The bytes that need to be filtered to the correct suffix</param>
        /// <param name="decimalPlaces">Set the decimal places for the suffix</param>
        /// <returns>The suffix string</returns>
        /// <exception cref="ArgumentOutOfRangeException">This exception is thrown when the decimalPlaces arg is zero or negative</exception>
        public static string SizeSuffix(long bytes, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces));
            switch (bytes)
            {
                case < 0:
                    return "-" + SizeSuffix(-bytes, decimalPlaces);
                case 0:
                    return string.Format("{0:n" + decimalPlaces + "} bytes", 0);
            }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(bytes, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)bytes / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }
    }
}