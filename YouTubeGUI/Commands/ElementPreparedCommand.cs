using System;
using Avalonia.Controls;
using Serilog;
using YouTubeGUI.Core;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Commands
{
    public class ElementPreparedCommand : CommandBase
    {
        public event Action<ContinuationItemRenderer>? ExecuteLoadContinuation;
        public override void Execute(object? parameter)
        {
            switch (parameter)
            {
                case ItemsRepeaterElementPreparedEventArgs irepea:
                    switch (irepea.Element.DataContext)
                    {
                        case ContinuationItemRenderer cir:
                            ExecuteLoadContinuation?.Invoke(cir);
                            Log.Information("Continuation found!");
                            break;
                    }
                    break;
            }
        }
    }
}