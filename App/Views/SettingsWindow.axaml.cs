using App.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace App.Views
{
    public partial class SettingsWindow : ReactiveWindow<SettingsWindowViewModel>
    {
        public SettingsWindow()
        {
            InitializeComponent();
            ViewModel = new SettingsWindowViewModel();
            this.WhenActivated(disposables =>
            {
                
            });
        }
        
    }
}