using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using YouTubeGUI.Controls;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Core.XamlTools
{
    public class DataTemplateSelector : IDataTemplate
    {
        [Content]
        public Dictionary<Type, IDataTemplate> Templates { get; set; } = new Dictionary<Type, IDataTemplate>();
        public bool IsItemTemplates { get; set; } = true;
        
        public IControl Build(object param)
        {
            switch (param)
            {
                case ContentRender cRenderer:
                    if (cRenderer.RichItem != null)
                    {
                        if (cRenderer.RichItem.RichItemContent.VideoRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(VideoRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(cRenderer.RichItem.RichItemContent.VideoRenderer);
                            }
                            Logger.Log("Could not find resource!", LogType.Warning);
                        }
                        if (cRenderer.RichItem.RichItemContent.RadioRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(RadioRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(cRenderer.RichItem.RichItemContent.RadioRenderer);
                            }
                            Logger.Log("Could not find resource!", LogType.Warning);
                        }
                        if (cRenderer.RichItem.RichItemContent.DisplayAdRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(DisplayAdRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(cRenderer.RichItem.RichItemContent.DisplayAdRenderer);
                            }
                            Logger.Log("Could not find resource!", LogType.Warning);
                        }
                    }
                    
                    if (cRenderer.RichSection != null)
                    {
                        if (cRenderer.RichSection.RichSectionContent.InlineSurveyRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(InlineSurveyRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(cRenderer.RichSection.RichSectionContent.InlineSurveyRenderer);
                            }
                            Logger.Log("Could not find resource!", LogType.Warning);
                        }
                        if (cRenderer.RichSection.RichSectionContent.PromotedItemRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(CompactPromotedItemRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(cRenderer.RichSection.RichSectionContent.PromotedItemRenderer);
                            }
                            Logger.Log("Could not find resource!", LogType.Warning);
                        }
                        if (cRenderer.RichSection.RichSectionContent.RichShelfRenderer != null)
                        {
                            if (App.Current.Styles.TryGetResource(typeof(RichShelfRenderer), out param))
                            {
                                var template = (IDataTemplate)param!;
                                return template.Build(cRenderer.RichSection.RichSectionContent.RichShelfRenderer);
                            }
                            Logger.Log("Could not find resource!", LogType.Warning);
                        }
                    }
                    
                    if (cRenderer.ContinuationItem != null)
                    {
                        if (App.Current.Styles.TryGetResource(typeof(ContinuationItemRenderer), out param))
                        {
                            var template = (IDataTemplate)param!;
                            return template.Build(cRenderer.ContinuationItem);
                        }
                        Logger.Log("Could not find resource!", LogType.Warning);
                    }
                    if (cRenderer.ChipCloudChip != null)
                    {
                        if (App.Current.Styles.TryGetResource(typeof(ChipCloudChipRenderer), out param))
                        {
                            var template = (IDataTemplate)param!;
                            return template.Build(cRenderer.ChipCloudChip);
                        }
                        Logger.Log("Could not find resource!", LogType.Warning);
                    }
                    return null;
                case ItemContents iContents:
                    if (!IsItemTemplates)
                    {
                        if (App.Current.Styles.TryGetResource(typeof(ItemContents), out param))
                        {
                            var template = (IDataTemplate)param!;
                            return template.Build(iContents);
                        }
                    }
                    return null;
                    break;
                case GuideItemRenderer giRenderer:
                    if (giRenderer.GuideSection != null)
                    {
                        if (App.Current.Styles.TryGetResource(typeof(GuideSection), out param))
                        {
                            var template = (IDataTemplate)param!;
                            return template.Build(giRenderer.GuideSection);
                        }
                    }
                    if (giRenderer.GuideSubscriptionSection != null)
                    {
                        if (App.Current.Styles.TryGetResource(typeof(GuideSubscriptionSection), out param))
                        {
                            var template = (IDataTemplate)param!;
                            return template.Build(giRenderer.GuideSubscriptionSection);
                        }
                    }
                    return null;
                default:
                    return null;
            }
        }

        public bool Match(object data)
        {
            return true;
        }
    }
}