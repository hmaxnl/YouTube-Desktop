using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Avalonia.Metadata;
using YouTubeGUI.Controls;
using YouTubeGUI.Controls.Content;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Core.XamlTools
{
    /* Is not used anymore! */
    public class DataTemplateSelector : IDataTemplate
    {
        [Content]
        public Dictionary<Type, IDataTemplate> Templates { get; set; } = new Dictionary<Type, IDataTemplate>();
        public bool IsItemTemplates { get; set; } = true;
        
        public IControl Build(object? param)
        {
            switch (param)
            {
                case RichVideoContent rvc:
                    if (App.Current.Resources.TryGetResource("HomeVideoItem", out param))
                    {
                        var template = (IDataTemplate)param!;
                        return template.Build(rvc);
                    }
                    break;
                case RadioRenderer rr:
                    if (App.Current.Resources.TryGetResource("HomeRadioItem", out param))
                    {
                        var template = (IDataTemplate)param!;
                        return template.Build(rr);
                    }
                    break;
                case DisplayAdRenderer dar:
                    if (App.Current.Resources.TryGetResource("HomeDisplayAd", out param))
                    {
                        var template = (IDataTemplate)param!;
                        return template.Build(dar);
                    }
                    break;
                case ContinuationItemRenderer cir:
                    if (App.Current.Resources.TryGetResource("ContinuationItem", out param))
                    {
                        var template = (IDataTemplate)param!;
                        return template.Build(cir);
                    }
                    break;

                case RichShelfRenderer rsr:
                    if (App.Current.Resources.TryGetResource("RichShelf", out param))
                    {
                        var template = (IDataTemplate)param!;
                        return template.Build(rsr);
                    }
                    break;
                case RichSectionRenderer richSectionRenderer: //TODO: Need datatemplate!
                    if (App.Current.Resources.TryGetResource("Default", out param))
                    {
                        var template = (IDataTemplate)param!;
                        return template.Build(richSectionRenderer);
                    }
                    break;
                case CompactPromotedItemRenderer cpir:
                    if (App.Current.Resources.TryGetResource("CompactPromotedItem", out param))
                    {
                        var template = (IDataTemplate)param!;
                        return template.Build(cpir);
                    }
                    break;
                case InlineSurveyRenderer isRenderer:
                    if (App.Current.Styles.TryGetResource("Default", out object? isRValue))
                    {
                        var template = (IDataTemplate)isRValue!;
                        return template.Build(isRenderer);
                    }
                    break;
                case ChipCloudChipRenderer cccRenderer:
                    if (App.Current.Styles.TryGetResource("Default", out object? cccRValue))
                    {
                        var template = (IDataTemplate)cccRValue!;
                        return template.Build(cccRenderer);
                    }
                    break;
            }
            return null;
        }

        public bool Match(object data)
        {
            return true;
        }
    }
}