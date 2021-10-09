using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace YouTubeScrap.Core.ReverseEngineer
{
    internal sealed class UserAuthentication
    {
        // Want to tank Dave Thomas @ https://stackoverflow.com/a/32065323/9948300 for
        // reverse engineering this and put it on stackoverflow as answer.
        public static AuthenticationHeaderValue GetSapisidHashHeader(string sapisid)
        {
            if (sapisid.IsNullEmpty())
                Trace.WriteLine("No SAPISD found! Could not make the SAPISIDHASH!");
            string time = GetTime();
            string sha1 = HashString($"{time} {sapisid} {DataManager.NetworkData.Origin}");
            string SAPISIDHASH_COMPLETE = $"{time}_{sha1}";
            return new AuthenticationHeaderValue("SAPISIDHASH", SAPISIDHASH_COMPLETE);
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