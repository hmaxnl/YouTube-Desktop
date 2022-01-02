using System;
using Avalonia.Controls;

namespace YouTubeGUI.Commands
{
    public class ScrollChangedCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter is not ScrollChangedEventArgs scea) return;
            if (scea.Source is ScrollViewer sv)
            {
                var vsv = sv.Offset.Length;
                var vsm = Math.Max(sv.Extent.Height - sv.Viewport.Height, 0);
                var offset = vsm - 300;
                if (vsv >= offset)
                    OnEndReached();
                //Logger.Log($"Max: {vsm} | Current: {vsv}", LogType.Debug);
            }
        }

        private void OnEndReached() => EndReached?.Invoke();

        public event Action? EndReached;
    }
}