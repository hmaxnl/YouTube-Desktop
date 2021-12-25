using YouTubeGUI.Stores;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentMainViewModel => MainContentNavigator.CurrentContentViewModel;

        public MainViewModel()
        {
            MainContentNavigator.ViewModelChanged += NavigatorOnViewModelChanged;
        }
        private void NavigatorOnViewModelChanged() => OnPropertyChanged(nameof(CurrentMainViewModel));

        public override void Dispose() => MainContentNavigator.ViewModelChanged -= NavigatorOnViewModelChanged;
    }
}