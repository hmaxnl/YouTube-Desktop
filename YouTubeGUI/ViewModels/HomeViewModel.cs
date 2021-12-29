using System.Collections.Generic;
using YouTubeGUI.Commands;
using YouTubeGUI.Core;
using YouTubeGUI.Models;
using YouTubeGUI.Models.Snippets;
using YouTubeGUI.Stores;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(YoutubeUser youtubeUser)
        {
            YoutubeUserStore.NotifyInitialRequestFinished += OnNotifyInitialRequestFinished;
            ScrollChangedCommand = new ScrollChangedCommand();
            ScrollChangedCommand.EndReached += CommandOnEndReached;
        }

        // Properties
        private HomeModel? HomeModel
        {
            get => _homeModel;
            set
            {
                _homeModel = value;
                if (HomeModel is { HomeSnippet: { } }) HomeModel.HomeSnippet.ContentsChanged += HomeSnippetOnContentsChanged;
            }
        }
        private HomeModel? _homeModel;
        
        public ScrollChangedCommand ScrollChangedCommand { get; }
        public List<RichItemRenderer>? ContentList => HomeModel?.HomeSnippet?.ItemContents;

        // Functions
        public override void Dispose() => YoutubeUserStore.NotifyInitialRequestFinished -= OnNotifyInitialRequestFinished;
        
        private void OnNotifyInitialRequestFinished(HomeSnippet? arg1, GuideSnippet? arg2) => HomeModel = new HomeModel(arg1, arg2);
        private void HomeSnippetOnContentsChanged()
        {
            // Notify properties from the 'home snippet' class.
            OnPropertyChanged(nameof(HomeModel));
            OnPropertyChanged(nameof(ContentList));
        }
        private void CommandOnEndReached()
        {
            Logger.Log("End reached!");
        }
    }
}