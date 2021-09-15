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
    //TODO: After making the user, call the API to get the user details. For now we can use the user to get logged in responses from the YouTube API.
    //TODO: Write the user/cookie functionality out in Obsidian or something to make the implementation more clear.
    //TODO: Make a system to save user to disk in binary or hashed JSON, and add a password protection to the file so not everybody can login only with the file. 
    public class YoutubeUser
    {
        public UserCookies UserCookies => userCookies;
        public UserData UserData;
        public UserSettings UserSettings;
        
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
        public YoutubeUser(UserCookies cookies)
        {
            userCookies = cookies;
            if (userCookies.CookieDictionary.TryGetValue("__Secure-3PAPISID", out Cookie cookie))
            {
                ExpirationDate = cookie.Expires;
                IsLoginExpired = cookie.Expired;
                userSAPISID = cookie.Value;
                //TODO: Set default user settings and make a request to get the user data.
            }
            else
                Trace.WriteLine("Could not acquire the SAPISID/__Secure-3PAPISID! User is unable to login!");
        }
        public void SaveUser()
        {
            //string pathToSave = Path.Combine(SettingsManager.Settings.UserStoragePath, UserData.UserID);
            //MemoryStream memStream = new MemoryStream();
            //BinaryFormatter binFormat = new BinaryFormatter();
            //binFormat.Serialize(memStream, UserCookies);
            //File.WriteAllBytes(Path.Combine(pathToSave, $"user_{UserData.UserID}.ytudata"), memStream.ToArray());
            //memStream.Dispose();
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
            {
                if (cookie.Domain != ".youtube.com") continue;
                userCookies.CookieDictionary[cookie.Name] = cookie;
                cookieHeaderBuilder.Append($"{cookie.Name}={cookie.Value}; ");
                Trace.WriteLine($"Added: {cookie.Name}");
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
        // Will be populated when there a settings implemented!
    }
}