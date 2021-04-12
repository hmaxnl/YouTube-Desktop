using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Handlers;

namespace YouTubeScrap.Core
{
    public static class LibraryExtensions
    {
        //public static bool IsValidVideoId(this string videoId)
        //{
        //    return (!!string.IsNullOrEmpty(videoId) || videoId.Length < 11);
        //}
        public static bool IsNullEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool ContainsKey(this string value, string key)// Checks if the key contains in the string (lowers the string and key, NOT case sensitive!)
        {
            return value.ToLower().IndexOf(key.ToLower()) != -1;
        }
        public static bool ContainsKeys(this string value, string[] keys)
        {
            foreach (string key in keys)
            {
                if (value.ContainsKey(key))
                    return true;
            }
            return false;
        }
        //public static int GetIndex(this ContentIdentifier contentIdentifier)
        //{
        //    return (int)contentIdentifier;
        //}
        //public static ContentIdentifier StringToEnum(string enumItem)
        //{
        //    return Enum.TryParse(enumItem, out ContentIdentifier result) ? result : ContentIdentifier.None;
        //}
        
    }
}