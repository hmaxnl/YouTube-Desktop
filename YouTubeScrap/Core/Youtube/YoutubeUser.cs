using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

using YouTubeScrap.Core.ReverseEngineer;

namespace YouTubeScrap.Core.Youtube
{
    public class YoutubeUser
    {
        public bool IsLoggedIn { get => requiredCookies.All(userCookies.CookieDictionary.ContainsKey) && !IsLoginExpired; }
        public UserCookies UserCookies { get => userCookies; }
        public readonly bool IsLoginExpired;
        public readonly DateTime ExpirationDate;
        private readonly string userSAPISID;
        private readonly UserCookies userCookies;
        private readonly string[] requiredCookies = new string[]
        {
            "SAPISID",
            "__Secure-3PSIDCC",
            "SIDCC"
        };
        public YoutubeUser(UserCookies loginCookies)
        {
            userCookies = loginCookies;
            if (userCookies.CookieDictionary.TryGetValue("SAPISID", out Cookie cookie))// Extract the SAPISID from cookie.
            {
                ExpirationDate = cookie.Expires;
                IsLoginExpired = ExpirationDate < DateTime.Now;
                userSAPISID = cookie.Value;
            }
        }
        public void GetUserData()
        {
            //TODO: Need to call the network handler for a request, deserialize the response and then populate the user class with the users data.
        }
        public AuthenticationHeaderValue GenerateAuthentication()
        {
            return UserAuthentication.GetSapisidHashHeader(userSAPISID);
        }
    }
    public struct UserCookies
    {
        public Dictionary<string, Cookie> CookieDictionary { get; set; }
        public string FinalizedLoginCookies { get; set; }
        public CookieContainer LoginCookieJar { get; set; }
    }
}