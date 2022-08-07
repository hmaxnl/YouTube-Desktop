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
#pragma warning disable 8618
        public MainWindowViewModel()
#pragma warning restore 8618
        {
            Activator = new ViewModelActivator();
            
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                DarkBtnCommand = ReactiveCommand.Create(() =>
                {
                    ExchangeManager.Exchange("ColorSchemes", "Dark");
                }).DisposeWith(disposables);
                LightBtnCommand = ReactiveCommand.Create(() =>
                {
                    ExchangeManager.Exchange("ColorSchemes", "Light");
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
        public ReactiveCommand<Unit, Unit> DarkBtnCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> LightBtnCommand { get; private set; }
    }
}
