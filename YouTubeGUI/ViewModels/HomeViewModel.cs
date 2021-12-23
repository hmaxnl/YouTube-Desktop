using System.Windows.Input;
using YouTubeGUI.Commands;
using YouTubeGUI.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public readonly ICommand LoadHomePageCommand;

        public HomeViewModel(YoutubeUser youtubeUser)
        {
            LoadHomePageCommand = new LoadPageCommandAsync(ApiRequestType.Home, youtubeUser, (a) =>
            {
                Logger.Log($"Action Update! Tracking: {a.TrackingParams}");
            });
            LoadHomePageCommand.Execute(null);
        }
        public override void Dispose()
        {
            
        }
    }
}