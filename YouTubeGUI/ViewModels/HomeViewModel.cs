using System.Windows.Input;
using YouTubeGUI.Commands;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public readonly ICommand LoadHomePageCommand;

        private ResponseMetadata _homeMetadata;
        public ResponseMetadata HomeMetadata
        {
            get => _homeMetadata;
            set
            {
                _homeMetadata = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel(YoutubeUser youtubeUser)
        {
            LoadHomePageCommand = new LoadPageCommandAsync(ApiRequestType.Home, youtubeUser, (a) =>
            {
                HomeMetadata = a;
            });
            LoadHomePageCommand.Execute(null);
        }
        public override void Dispose()
        {
            
        }
    }
}