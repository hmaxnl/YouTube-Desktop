using System;
using System.Reactive.Disposables;
using App.Models;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Platform;
using ReactiveUI;

namespace App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IActivatableViewModel
    {
#pragma warning disable 8618
        public MainWindowViewModel()
#pragma warning restore 8618
        {
            Activator = new ViewModelActivator();
            
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                /* Activation */
                Disposable.Create(() =>
                {
                    /* Disposing */
                }).DisposeWith(disposables);
            });
            // Create a new session.
            Sessions.Add(new Session(Bootstrap.TestUser));
        }
        
        public WindowIcon Icon { get; set; } = new WindowIcon(AvaloniaLocator.Current.GetService<IAssetLoader>()?.Open(new Uri(@"avares://App/Assets/avalonia-logo.ico")));
        public ViewModelActivator Activator { get; }
        public AvaloniaList<Session> Sessions { get; } = new AvaloniaList<Session>();
    }
}
