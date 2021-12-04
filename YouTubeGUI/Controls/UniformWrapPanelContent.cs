using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Controls
{
    public class UniformWrapPanelContent : WrapPanel
    {
        public UniformWrapPanelContent()
        {
            
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count > 0)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    double totalWidth = availableSize.Width;
                    ItemWidth = 0.0;
                    foreach (IControl el in Children)
                    {
                        el.Measure(availableSize);
                        Size next = el.DesiredSize;
                        if (!(Double.IsInfinity(next.Width) ||
                              Double.IsNaN(next.Width)))
                        {
                            ItemWidth = Math.Max(next.Width, ItemWidth);
                        }
                    }
                }
                else
                {
                    double totalHeight = availableSize.Height;
                    ItemHeight = 0.0;
                    foreach (IControl el in Children)
                    {
                        el.Measure(availableSize);
                        Size next = el.DesiredSize;
                        if (!(Double.IsInfinity(next.Height) ||
                              Double.IsNaN(next.Height)))
                        {
                            ItemHeight = Math.Max(next.Height, ItemHeight);
                        }
                    }
                }
            }
            return base.MeasureOverride(availableSize);
        }
    }
}