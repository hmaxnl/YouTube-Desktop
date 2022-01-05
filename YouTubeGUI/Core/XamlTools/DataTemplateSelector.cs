using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using YouTubeGUI.Controls;
using YouTubeGUI.Controls.Content;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Core.XamlTools
{
    public class DataTemplateSelector : IDataTemplate
    {
        [Content]
        public Dictionary<Type, IDataTemplate> Templates { get; set; } = new Dictionary<Type, IDataTemplate>();
        public bool IsItemTemplates { get; set; } = true;
        
        public IControl Build(object? param)
        {
            switch (param)
            {
                case RichItemRenderer rItemRenderer:
                    switch (rItemRenderer.Content)
                    {
                        case RichVideoContent:
                            return new ItemRenderer("HomeVideo");
                        case RadioRenderer:
                            return new ItemRenderer("HomeRadio");
                        case DisplayAdRenderer:
                            return new ItemRenderer("HomeDisplayAd");
                        case ContinuationItemRenderer:
                            return new ItemRenderer("HomeContinuation");
                    }
                    break;
                case ItemContents iContents:
                    if (!IsItemTemplates)
                    {
                        if (App.Current.Styles.TryGetResource(typeof(ItemContents), out param))
                        {
                            var template = (IDataTemplate)param!;
                            return template.Build(iContents);
                        }
                    }
                    break;
                case InlineSurveyRenderer isRenderer:
                    if (App.Current.Styles.TryGetResource(typeof(InlineSurveyRenderer), out object? isRValue))
                    {
                        var template = (IDataTemplate)isRValue!;
                        return template.Build(isRenderer);
                    }
                    break;
                case CompactPromotedItemRenderer cpiRenderer:
                    if (App.Current.Styles.TryGetResource(typeof(CompactPromotedItemRenderer), out object? cpiRValue))
                    {
                        var template = (IDataTemplate)cpiRValue!;
                        return template.Build(cpiRenderer);
                    }
                    break;
                case RichShelfRenderer rsRenderer:
                    if (App.Current.Styles.TryGetResource(typeof(RichShelfRenderer), out object? rsRValue))
                    {
                        var template = (IDataTemplate)rsRValue!;
                        return template.Build(rsRenderer);
                    }
                    break;
                case ContinuationItemRenderer ciRenderer:
                    if (App.Current.Styles.TryGetResource(typeof(ContinuationItemRenderer), out object? ciRValue))
                    {
                        var template = (IDataTemplate)ciRValue!;
                        return template.Build(ciRenderer);
                    }
                    break;
                case ChipCloudChipRenderer cccRenderer:
                    if (App.Current.Styles.TryGetResource(typeof(ChipCloudChipRenderer), out object? cccRValue))
                    {
                        var template = (IDataTemplate)cccRValue!;
                        return template.Build(cccRenderer);
                    }
                    break;
               
                case GuideItemRenderer giRenderer:
                    if (giRenderer.GuideSection != null)
                    {
                        if (!IsItemTemplates)
                        {
                            if (!App.Current.Styles.TryGetResource(typeof(GuideSection), out param)) return null;
                            var template = (IDataTemplate)param!;
                            return template.Build(giRenderer.GuideSection);
                        }
                    }
                    if (giRenderer.GuideSubscriptionSection != null)
                    {
                        if (!IsItemTemplates)
                        {
                            if (!App.Current.Styles.TryGetResource(typeof(GuideSubscriptionSection), out param)) return null;
                            var template = (IDataTemplate)param!;
                            return template.Build(giRenderer.GuideSubscriptionSection);
                        }
                    }
                    break;
                case GuideEntry gEntry:
                    if (gEntry != null)
                    {
                        if (gEntry.GuideEntryRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(GuideEntryRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(gEntry.GuideEntryRenderer);
                            }
                        }

                        if (gEntry.GuideCollapsibleEntryRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(GuideCollapsibleEntryRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(gEntry.GuideCollapsibleEntryRenderer);
                            }
                        }

                        if (gEntry.GuideDownloadsEntryRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(GuideDownloadsEntryRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(gEntry.GuideDownloadsEntryRenderer);
                            }
                        }

                        if (gEntry.GuideCollapsibleSectionEntryRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(GuideCollapsibleSectionEntryRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(gEntry.GuideCollapsibleSectionEntryRenderer);
                            }
                        }
                    }
                    return null;
            }
            return null;
        }

        public bool Match(object data)
        {
            return true;
        }
    }
}