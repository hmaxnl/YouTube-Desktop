using System.Reactive.Disposables;
using App.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace App.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, viewModel => viewModel.Greeting, view => view.GreetTextBlock.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, viewModel => viewModel.Title, view => view.Title)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, viewModel => viewModel.Icon, view => view.Icon).DisposeWith(disposables);
            });
        }
    }
}