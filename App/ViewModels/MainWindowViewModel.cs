using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using ReactiveUI;

namespace App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IActivatableViewModel
    {
        public string Greeting => "Welcome to Avalonia!";
        public ViewModelActivator Activator { get; }

        public MainWindowViewModel()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                /* Activation */
                Disposable.Create(() =>
                {
                    /* disposing */
                }).DisposeWith(disposables);
            });
        }
    }
}
