using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IContent : ITrackingParams, IIdentifier
    {
        [JsonProperty("kind")]
        ContentIdentifier Kind { get; set; }
    }
}