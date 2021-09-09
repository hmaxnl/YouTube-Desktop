using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core.ReverseEngineer;

namespace YouTubeScrap.Core.Youtube
{
    public class YoutubeUser
    {
        public UserCookies UserCookies => userCookies;
        public UserData UserData;
        public UserSettings UserSettings;
        
        public readonly bool IsLoginExpired;
        public readonly DateTime ExpirationDate;
        
        public bool IsLoggedIn => requiredCookies.All(userCookies.CookieDictionary.ContainsKey) && !IsLoginExpired;
        
        private readonly string userSAPISID;
        private readonly UserCookies userCookies;
        private readonly string[] requiredCookies = new string[]
        {
            "SAPISID",
            "__Secure-3PSIDCC",
            "SIDCC"
        };
        public YoutubeUser(UserCookies cookies)
        {
            userCookies = cookies;
            if (userCookies.CookieDictionary.TryGetValue("SAPISID", out Cookie cookie))
            {
                ExpirationDate = cookie.Expires;
                IsLoginExpired = cookie.Expired;
                userSAPISID = cookie.Value;
                //TODO: Set default user settings and make a request to get the user data.
            }
            else
                Trace.WriteLine("Could not acquire the SAPISID! User is unable to login!");
        }
        public void SaveUser()
        {
            string pathToSave = Path.Combine(SettingsManager.Settings.UserStoragePath, UserData.UserID);
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(memStream, UserCookies);
            File.WriteAllBytes(Path.Combine(pathToSave, $"user_{UserData.UserID}.ytudata"), memStream.ToArray());
            memStream.Dispose();
        }
        public static YoutubeUser LoadUser()
        {
            return null;
        }

        public static UserCookies FilterCookies(CookieCollection cookieJar)
        {
            UserCookies userCookies = new UserCookies() { CookieJar = cookieJar, CookieDictionary = new Dictionary<string, Cookie>() };
            StringBuilder cookieHeaderBuilder = new StringBuilder();
            foreach (Cookie cookie in cookieJar)
            {// Hmm... not really happy with this piece of code, but it works for now!
                if (!userCookies.CookieDictionary.ContainsKey(cookie.Name))
                {
                    Trace.WriteLine($"Added: {cookie.Name}");
                    userCookies.CookieDictionary.Add(cookie.Name, cookie);
                }
                else // Compare the cookies with each other to see if they have the same value or different domains.
                {
                    if (userCookies.CookieDictionary.TryGetValue(cookie.Name, out Cookie compCookie))
                    { // Skip the cookie if they have the same value.
                        if (compCookie.Value != cookie.Value)
                        {
                            if (compCookie.Domain != cookie.Domain) // If one of the cookies has the '.youtube.com' domain we choose that one, else we skip all this and hope for the best!
                            {
                                Cookie tempCookie = (cookie.Domain == ".youtube.com") ? cookie : (compCookie.Domain == ".youtube.com") ? compCookie : null; // Haha one-liner go brrr
                                if (tempCookie != null)
                                {
                                    Trace.WriteLine($"Replace cookie: {cookie.Name}");
                                    userCookies.CookieDictionary[cookie.Name] = tempCookie;
                                }
                            }
                        }
                    }
                    else
                        Trace.WriteLine("Could not get cookie from the dictionary!");
                }
                cookieHeaderBuilder.Append($"{cookie.Name}={cookie.Value}; ");
            }
            userCookies.CookiesHeader = cookieHeaderBuilder.ToString();
            return userCookies;
        }
        public void GetUserData()
        {
            //TODO: Need to call the network handler for a request, deserialize the response and then populate the user class with the users data.
        }

        public static UserCookies ReadCookies()
        {// For testing only!
            Trace.WriteLine("Reading cookies...");
            string userCookiesJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Cookies.json"));
            return JsonConvert.DeserializeObject<UserCookies>(userCookiesJson);
        }
        public void SaveCookies()
        {// For testing only!
            Trace.WriteLine("Saving cookies...");
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Cookies.json"), JObject.FromObject(UserCookies).ToString());
        }
        public AuthenticationHeaderValue GenerateAuthentication()
        {
            return UserAuthentication.GetSapisidHashHeader(userSAPISID);
        }
    }
    [Serializable]
    public struct UserCookies
    {
        [JsonProperty("CookieJar")]
        public CookieCollection CookieJar { get; set; }
        [JsonProperty("CookieDictionary")]
        public Dictionary<string, Cookie> CookieDictionary { get; set; }
        [JsonProperty("CookieHeader")]
        public string CookiesHeader { get; set; }
    }
    public struct UserData
    {
        public string UserID { get; set; } // User/Channel ID.
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
    }
    public struct UserSettings
    {
        
    }
}