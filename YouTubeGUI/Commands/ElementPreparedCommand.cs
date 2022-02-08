using System;
using Avalonia.Controls;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Commands
{
    public class ElementPreparedCommand : CommandBase
    {
        public event Action? ExecuteLoadContinuation;
        public override void Execute(object? parameter)
        {
            switch (parameter)
            {
                case ItemsRepeaterElementPreparedEventArgs irepea:
                    switch (irepea.Element.DataContext)
                    {
                        case ContinuationItemRenderer:
                            ExecuteLoadContinuation?.Invoke();
                            break;
                    }
                    break;
            }
        }
    }
}