using Avalonia.Controls;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Themes.Default.Resources
{
    public class Resources : ResourceDictionary
    {
        private void HomeREF_OnSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            switch (e.DataContext)
            {
                // Items
                case RichVideoContent:
                    e.TemplateKey = "VideoItem";
                    break;
                case RadioRenderer:
                    e.TemplateKey = "RadioItem";
                    break;
                case DisplayAdRenderer:
                    e.TemplateKey = "DisplayAd";
                    break;
                case ContinuationItemRenderer:
                    e.TemplateKey = "ContinuationItem";
                    break;
                // Sections
                case RichShelfRenderer:
                    e.TemplateKey = "Shelf";
                    break;
                case CompactPromotedItemRenderer:
                    e.TemplateKey = "PromotedItem";
                    break;
                case InlineSurveyRenderer:
                default:
                    e.TemplateKey = "DefItem";
                    break;
            }
        }
    }
}