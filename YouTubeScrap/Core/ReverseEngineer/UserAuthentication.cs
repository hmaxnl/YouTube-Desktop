using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Management;
using Serilog;

namespace YouTubeScrap.Core.ReverseEngineer
{
    internal static class UserAuthentication
    {
        // Dave Thomas @ https://stackoverflow.com/a/32065323/9948300
        public static AuthenticationHeaderValue GetSapisidHashHeader(string sapisid)
        {
            if (sapisid.IsNullEmpty())
                Log.Warning("No SAPISD found! Could not make the SAPISIDHASH!");
            string time = GetTime();
            string sha1 = HashString($"{time} {sapisid} {Manager.Properties.GetString("Origin")}");
            string sapisidhashComplete = $"{time}_{sha1}";
            return new AuthenticationHeaderValue("SAPISIDHASH", sapisidhashComplete);
        }
        private static string HashString(string password)
        {
            var dataBytes = Encoding.ASCII.GetBytes(password);
            var hashData = new SHA1Managed().ComputeHash(dataBytes);
            string sha1 = string.Empty;
            foreach (var item in hashData)
                sha1 += item.ToString("x2");
            return sha1;
        }
        private static string GetTime()
        {
            DateTime st = new DateTime(1970, 1, 1);
            TimeSpan t = DateTime.Now.ToUniversalTime() - st;
            string retval = (t.TotalMilliseconds + 0.5).ToString();
            return retval.Substring(0, 10);
        }
    }
}