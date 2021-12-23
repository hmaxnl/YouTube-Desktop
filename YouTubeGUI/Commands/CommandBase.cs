using System;
using System.Windows.Input;

namespace YouTubeGUI.Commands
{
    public abstract class CommandBase : ICommand
    {
        public virtual bool CanExecute(object? parameter) => true;

        public abstract void Execute(object? parameter);

        protected void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler? CanExecuteChanged;
    }
}