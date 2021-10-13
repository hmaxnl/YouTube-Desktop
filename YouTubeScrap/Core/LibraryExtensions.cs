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
        public static bool ContainsKey(this string value, string key, bool keepCase = false)// Checks if the key contains in the string.
        {
            if (!keepCase)
                return value.ToLower().IndexOf(key.ToLower(), StringComparison.Ordinal) != -1;
            return value.IndexOf(key, StringComparison.Ordinal) != -1;
        }
        public static bool ContainsKeys(this string value, string[] keys, bool keepCase = false)// Check if any of the given keys is in the string.
        {
            return keys.Any(key => value.ContainsKey(key, keepCase));
        }
        public static Dictionary<string, int> Getkeys(this string value, string[] keys, bool keepCase = false)
        {
            Dictionary<string, int> resultDict = new Dictionary<string, int>();
            for (int i = 0; i < keys.Length; i++)
            {
                int val = value.IndexOf(keys[i], StringComparison.Ordinal);
                if (val != -1 & val != 0)
                    resultDict.Add(keys[i], val);
            }
            return resultDict;
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