using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Google.YouTube
{
    public class YTVideo
    {
        public Video GetVideoInfo(string videoID)
        {
            Video vid = null;

            var videoRequest = YTService.GetService.Videos.List("snippet");
            videoRequest.Id = videoID;
            var response = videoRequest.Execute();
            if (response.Items.Count == 1)
                foreach (Video item in response.Items)
                {
                    vid = item;
                }
            return vid;
        }
    }
}