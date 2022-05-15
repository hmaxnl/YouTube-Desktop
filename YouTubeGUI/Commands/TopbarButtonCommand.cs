using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using YouTubeGUI.Models;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Commands
{
    public class TopbarButtonCommand : CommandBase
    {
        public TopbarButtonCommand(Session session) => _session = session;
        private Session _session;
        
        public override void Execute(object? parameter)
        {
            if (parameter == null) return;
            switch (parameter)
            {
                case Button btn:
                    if (btn.ContextFlyout != null) // If the button already has a flyout show it.
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
            // Filter on data context.
            switch (control.DataContext)
            {
                case MenuButtonRenderer mbr:
                    if (mbr.MenuRequest != null)
                    {
                        /*ResponseMetadata? metaData = _session.Workspace.WorkspaceUser
                                .GetApiMetadataAsync(ApiRequestType.Custom).Result;*/
                        //TODO: Make request and set the content!!!
                        return null;
                    }
                    if (mbr.MenuRenderer != null)
                    {
                        return new MenuFlyout()
                        {
                            ShowMode = FlyoutShowMode.Standard,
                            Items = mbr.MenuRenderer.MultiPageMenuRenderer.Sections,
                            Placement = FlyoutPlacementMode.BottomEdgeAlignedRight,
                            FlyoutPresenterClasses = { "TopbarButtonFlyout" } // Load classes that a created as xaml style/theme.
                        };
                    }
                    return null;
                default:
                    return null;
            }
        }
    }
}