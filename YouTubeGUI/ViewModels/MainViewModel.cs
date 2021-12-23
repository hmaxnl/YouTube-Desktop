using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private YoutubeUser _currentUser;

        public ViewModelBase CurrentMainViewModel => Navigator.CurrentContentViewModel;

        public MainViewModel()
        {
            Navigator.ViewModelChanged += NavigatorOnViewModelChanged;
            _currentUser = new YoutubeUser(YoutubeUser.ReadCookies());
            Navigator.CurrentContentViewModel = new HomeViewModel(_currentUser);
        }
        private void NavigatorOnViewModelChanged() => OnPropertyChanged(nameof(CurrentMainViewModel));

        public override void Dispose() => Navigator.ViewModelChanged -= NavigatorOnViewModelChanged;
    }
}