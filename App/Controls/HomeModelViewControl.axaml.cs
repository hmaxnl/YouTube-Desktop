using App.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace App.Controls
{
    public partial class HomeModelViewControl : ReactiveUserControl<HomeViewModel>
    {
        // Controls
        // Avalonia docs: https://docs.avaloniaui.net/guides/deep-dives/reactiveui/view-activation#code-behind-reactiveui-bindings
        private TextBlock MainText => this.FindControl<TextBlock>("XamlMainTextBlock");

        // The designer can only create a instance if there is a parameterless constructor!
        public HomeModelViewControl() { }
        public HomeModelViewControl(HomeViewModel? model = null)
        {
            InitializeComponent();
            ViewModel = model ?? new HomeViewModel();
            
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, viewModel => viewModel.Title, view => view.MainText.Text).DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}