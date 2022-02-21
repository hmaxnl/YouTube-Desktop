using System;

namespace YouTubeGUI.Commands
{
    public class NavigationPaneCommand : CommandBase
    {
        public event Action? TogglePane;
        public override void Execute(object? parameter) => TogglePane?.Invoke();
    }
}