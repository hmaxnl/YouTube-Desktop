using Avalonia.Controls;
using Avalonia.Interactivity;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Themes.Default.Resources
{
    public class DataTemplates : ResourceDictionary
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

        private void GuideItemRef_OnSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            switch (e.DataContext)
            {
                case GuideEntryRenderer:
                    e.TemplateKey = "Entry";
                    break;
                case GuideCollapsibleEntryRenderer:
                    e.TemplateKey = "CollapsibleEntry";
                    break;
                case GuideDownloadsEntryRenderer:
                    e.TemplateKey = "DownloadsEntry";
                    break;
                case GuideSigninPromoRenderer:
                    e.TemplateKey = "SignInPromo";
                    break;
                case GuideSection:
                    e.TemplateKey = "GuideSection";
                    break;
                case GuideSubscriptionSection:
                    e.TemplateKey = "GuideSubsSection";
                    break;
                case GuideCollapsibleSectionEntryRenderer:
                    e.TemplateKey = "GuideCollapsibleSection";
                    break;
                default:
                    e.TemplateKey = "DefItem";
                    break;
            }
        }

        private void ItemsRepeater_OnElementClearing(object? sender, ItemsRepeaterElementClearingEventArgs e)
        {
            if (e.Element != null)
            {
                e.Element.DataContext = null;
            }
        }

        private void TopbarButtonsRef_OnSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            switch (e.DataContext)
            {
                case ButtonRenderer btnRenderer:
                    switch (btnRenderer.TargetId)
                    {
                        case "topbar-signin":
                            e.TemplateKey = "TopbarLoginButton";
                            break;
                        default:
                            e.TemplateKey = "TopbarButton";
                            break;
                    }
                    break;
                case MenuButtonRenderer:
                    e.TemplateKey = "TopbarMenuButton";
                    break;
                case NotificationTopbarButtonRenderer:
                    e.TemplateKey = "TopbarNotificationButton";
                    break;
                default:
                    e.TemplateKey = "DefItem";
                    break;
            }
        }

        /* Topbar templates */
        private void TopbarButtonFlyoutRef_OnSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            switch (e.DataContext)
            {
                case CompactLink:
                    e.TemplateKey = "CompactLink";
                    break;
                default:
                    e.TemplateKey = "DefItem";
                    break;
            }
            
        }
    }
}