using System;

namespace YouTubeGUI.Commands
{
    public class ViewInitializedCommand : CommandBase
    {
        public event Action<EventArgs?>? ViewInitialized;
        public override void Execute(object? parameter) => ViewInitialized?.Invoke(parameter as EventArgs);
    }
}