using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Themes.Default
{
    public class DataTemplateFilter : IDataTemplate
    {
        public Type DataType { get; set; }
        [Content]
        public Dictionary<Type, IDataTemplate> Templates { get; } = new Dictionary<Type, IDataTemplate>();

        public IControl Build(object param)
        {
            switch (param)
            {
                case ContentRender renderer:
                    if (renderer.RichItem != null)
                    {
                        if (renderer.RichItem.RichItemContent.VideoRenderer != null)
                        {
                            var template = Templates[typeof(VideoRenderer)];
                            return template.Build(renderer.RichItem.RichItemContent.VideoRenderer);
                        }

                        if (renderer.RichItem.RichItemContent.RadioRenderer != null)
                        {
                            var template = Templates[typeof(RadioRenderer)];
                            return template.Build(renderer.RichItem.RichItemContent.RadioRenderer);
                        }
                        if (renderer.RichItem.RichItemContent.DisplayAdRenderer != null)
                        {
                            var template = Templates[typeof(DisplayAdRenderer)];
                            return template.Build(renderer.RichItem.RichItemContent.DisplayAdRenderer);
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