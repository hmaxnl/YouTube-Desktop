using Newtonsoft.Json;

namespace YouTubeScrap.Data.Interfaces
{
    public interface IContentRenderer
    {
        [JsonProperty("rendererType")]
        ContentRenderer ContentRendererType { get; }
    }

    public enum ContentRenderer
    {
        RichItemRenderer,
        RichSectionRenderer,
        ContinuationItemRenderer
    }
}