using System;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IIdentifier
    {
        [JsonIgnore]
        Type Identifier { get; }
    }
}