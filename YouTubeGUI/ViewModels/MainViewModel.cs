using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Navigator _navigator;
        private YoutubeUser _currentUser;

        public ViewModelBase CurrentBase => _navigator.CurrentViewModel;

        public MainViewModel(Navigator navg)
        {
            _navigator = navg;
            _navigator.ViewModelChanged += NavigatorOnViewModelChanged;
            _currentUser = new YoutubeUser(YoutubeUser.ReadCookies());
        }
        private void NavigatorOnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentBase));
        }
    }
}