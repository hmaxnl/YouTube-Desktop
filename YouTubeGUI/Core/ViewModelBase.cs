using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
using JetBrains.Annotations;

namespace YouTubeGUI.Core
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Used for UI things.
        public void AskDispatcher(Action a)
        {
            if (!Dispatcher.UIThread.CheckAccess())
                Dispatcher.UIThread.Post(a);
            else
                a();
        }
    }
}