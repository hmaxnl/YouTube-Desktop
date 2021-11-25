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
            /*switch (param)
            {
                case ListBoxItem listBoxItem:
                    switch (listBoxItem.Content)
                    {
                        case ContentRender cRenderer:
                            if (cRenderer.RichItem == null) return null;
                            if (cRenderer.RichItem.RichItemContent.VideoRenderer != null)
                            {
                                var template = Templates[typeof(VideoRenderer)];
                                return template.Build(param);
                            }
                            break;
                    }
                    break;
                default:
                    return new ControlTemplateResult(new Border(), new NameScope());
            }
            return new ControlTemplateResult(new Border(), new NameScope());*/
            var template = Templates[typeof(VideoRenderer)];
            return template.Build(param);
        }
    }
}