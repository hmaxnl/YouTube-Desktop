using System;
using System.Reactive;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using ReactiveUI;

namespace App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IActivatableViewModel
    {
        public MainWindowViewModel()
        {
            Activator = new ViewModelActivator();
            
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                DarkBtnCommand = ReactiveCommand.Create(() =>
                {
                    
                }).DisposeWith(disposables);
                LightBtnCommand = ReactiveCommand.Create(() =>
                {
                    
                }).DisposeWith(disposables);
                /* Activation */
                Disposable.Create(() =>
                {
                    /* Disposing */
                }).DisposeWith(disposables);
            });
        }
        
        public string Greeting => "Welcome to Avalonia!";
        public string Title { get; set; } = "Application";
        public WindowIcon Icon { get; set; } = new WindowIcon(AvaloniaLocator.Current.GetService<IAssetLoader>()?.Open(new Uri(@"avares://App/Assets/avalonia-logo.ico")));
        public ViewModelActivator Activator { get; }
        public ReactiveCommand<Unit, Unit> DarkBtnCommand { get; set; }
        public ReactiveCommand<Unit, Unit> LightBtnCommand { get; set; }
    }
}
