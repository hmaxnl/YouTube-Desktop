using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using Serilog;
using YouTubeGUI.ViewModels;
using YouTubeGUI.Windows;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Commands
{
    public class TopbarButtonCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter == null) return;
            switch (parameter)
            {
                case Button btn:
                    if (btn.ContextFlyout != null)
                    {
                        btn.ContextFlyout.ShowAt(btn);
                        return;
                    }
                    FlyoutBase? flyoutBase = BuildFlyout(btn);
                    if (flyoutBase == null) return;
                    btn.ContextFlyout = flyoutBase;
                    btn.ContextFlyout.ShowAt(btn);
                    break;
            }
        }

        private static FlyoutBase? BuildFlyout(IControl? control)
        {
            if (control == null) return null;
            switch (control.DataContext)
            {
                case MenuButtonRenderer mbr:
                    if (mbr.MenuRequest != null)
                    {
                        var vRoot = control.GetVisualRoot();
                        if (vRoot is MainWindow { DataContext: MainViewModel mvm })
                        {
                            /*ResponseMetadata? metaData = mvm.Session.Workspace.WorkspaceUser
                                .GetApiMetadataAsync(ApiRequestType.Custom).Result;*/
                        }
                        Log.Information("MenuRequest hit!");
                        //TODO: Make request and set the content!!!
                        return null;
                    }
                    Log.Information("MenuRenderer hit!");
                    return new MenuFlyout()
                    {
                        ShowMode = FlyoutShowMode.Standard,
                        Items = mbr.MenuRenderer.MultiPageMenuRenderer.Sections,
                        Placement = FlyoutPlacementMode.BottomEdgeAlignedRight,
                        FlyoutPresenterClasses = { "TopbarButtonFlyout" }
                    };
                default:
                    return null;
            }
        }
    }
}