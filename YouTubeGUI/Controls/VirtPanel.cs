using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace YouTubeGUI.Controls
{
    public class VirtPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            if (double.IsInfinity(availableSize.Height))
            {
                var c = this.GetVisualAncestors().OfType<IControl>().FirstOrDefault(v => v.IsArrangeValid && !v.Bounds.IsEmpty);
                if (c != null)
                {
                    availableSize = availableSize.WithHeight(c.Bounds.Height);
                }
            }
            return base.MeasureOverride(availableSize);
        }
    }
}