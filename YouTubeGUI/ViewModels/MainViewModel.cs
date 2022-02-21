using YouTubeGUI.Commands;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            MainContentNavigator.ViewModelChanged += NavigatorOnViewModelChanged;
            /* Commands */
            NavigationPaneCommand = new NavigationPaneCommand();
            NavigationPaneCommand.TogglePane += () => { IsGuidePaneOpen = !IsGuidePaneOpen; OnPropertyChanged(nameof(IsGuidePaneOpen)); };
        }

        public string WindowTitle { get; set; } = "YouTube Desktop";
        // Current content viewmodel
        public ViewModelBase CurrentContentViewModel => MainContentNavigator.CurrentContentViewModel;
        /* Commands */
        public NavigationPaneCommand NavigationPaneCommand { get; set; }
        public bool IsGuidePaneOpen { get; set; } = true;
        
        // Privates
        private void NavigatorOnViewModelChanged() => OnPropertyChanged(nameof(CurrentContentViewModel));
        public override void Dispose() => MainContentNavigator.ViewModelChanged -= NavigatorOnViewModelChanged;
    }
}