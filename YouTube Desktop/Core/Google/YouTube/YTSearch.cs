using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.YouTube.v3.Data;

namespace YouTube_Desktop.Core.Google.YouTube
{
    public class YTSearch
    {
        public async Task<SearchListResponse> SearchYTAsync(string searchQ)
        {
            var slr = YTService.GetService.Search.List("snippet");
            slr.Q = searchQ;
            slr.MaxResults = 64;

            var slresponse = await slr.ExecuteAsync();
            return slresponse;
        }
    }
}