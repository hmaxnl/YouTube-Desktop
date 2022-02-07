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
                case RichShelfRenderer:
                    e.TemplateKey = "Shelf";
                    break;
                case RadioRenderer:
                    e.TemplateKey = "RadioItem";
                    break;
                case DisplayAdRenderer:
                    e.TemplateKey = "DisplayAd";
                    break;
                // Sections
                case RichSectionRenderer:
                    e.TemplateKey = "Section";
                    break;
                case CompactPromotedItemRenderer:
                    e.TemplateKey = "PromotedItem";
                    break;
                case ContinuationItemRenderer:
                    e.TemplateKey = "ContinuationItem";
                    break;
                default:
                    e.TemplateKey = "DefItem";
                    break;
            }
        }
    }
}