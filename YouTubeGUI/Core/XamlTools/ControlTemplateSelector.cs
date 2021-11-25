using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Avalonia.Styling;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Core.XamlTools
{
    public class ControlTemplateSelector : IControlTemplate
    {
        [Content]
        public Dictionary<Type, IControlTemplate> Templates { get; } = new Dictionary<Type, IControlTemplate>();
        public ControlTemplateResult Build(ITemplatedControl param)
        {
            switch (param)
            {
                case ListBoxItem listBoxItem:
                    switch (listBoxItem.Content)
                    {
                        case ContentRender cRenderer:
                            if (cRenderer.RichItem != null)
                            {
                                if (cRenderer.RichItem.RichItemContent.VideoRenderer != null)
                                {
                                    var template = Templates[typeof(VideoRenderer)];
                                    return template.Build(param);
                                }

                                if (cRenderer.RichItem.RichItemContent.RadioRenderer != null)
                                {
                                    var template = Templates[typeof(RadioRenderer)];
                                    return template.Build(param);
                                }

                                if (cRenderer.RichItem.RichItemContent.DisplayAdRenderer != null)
                                {
                                    var template = Templates[typeof(DisplayAdRenderer)];
                                    return template.Build(param);
                                }
                            }

                            if (cRenderer.ContinuationItem != null)
                            {
                                var template = Templates[typeof(ContinuationItemRenderer)];
                                return template.Build(param);
                            }

                            if (cRenderer.RichSection != null)
                            {
                                if (cRenderer.RichSection.RichSectionContent.InlineSurveyRenderer != null)
                                {
                                    var template = Templates[typeof(InlineSurveyRenderer)];
                                    return template.Build(param);
                                }
                                if (cRenderer.RichSection.RichSectionContent.PromotedItemRenderer != null)
                                {
                                    var template = Templates[typeof(CompactPromotedItemRenderer)];
                                    return template.Build(param);
                                }
                                if (cRenderer.RichSection.RichSectionContent.RichShelfRenderer != null)
                                {
                                    var template = Templates[typeof(RichShelfRenderer)];
                                    return template.Build(param);
                                }
                            }

                            if (cRenderer.ChipCloudChip != null)
                            {
                                var template = Templates[typeof(ChipCloudChipRenderer)];
                                return template.Build(param);
                            }
                            
                            break;
                    }
                    break;
                default:
                    return new ControlTemplateResult(new Border(), new NameScope());
            }
            return new ControlTemplateResult(new Border(), new NameScope());
        }
    }
}