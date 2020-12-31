using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.YouTube.v3;
using Google.Apis.Services;

namespace YouTube_Desktop.Core.Google.YouTube
{
    public static class YTService
    {
        private static YouTubeService _appYTServ;
        
        public static void SetupService()
        {
            _appYTServ = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDWpSeo6MbFO6H8qWj5GYjSu32r7IbZOJc",
                ApplicationName = "YouTube Desktop"
            });
        }
        public static YouTubeService GetService
        {
            get 
            {
                if(_appYTServ == null)
                    SetupService();
                return _appYTServ;
            }

        }

        static void test()
        {
            return;
        }
    }
}