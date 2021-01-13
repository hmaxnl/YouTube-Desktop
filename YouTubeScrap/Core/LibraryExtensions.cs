using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

using YouTubeScrap.Core.Exceptions;

namespace YouTubeScrap.Core
{
    public static class LibraryExtensions
    {
        /// <summary>
        /// For validating video is.
        /// NOTE: Only use on youtube video id's!
        /// </summary>
        /// <param name="videoId">The video id.</param>
        /// <returns></returns>
        public static bool IsValidVideoId(this string videoId)
        {
            return (!!string.IsNullOrEmpty(videoId) || videoId.Length < 11);
        }
    }
}