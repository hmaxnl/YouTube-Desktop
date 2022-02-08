using System.Collections.Generic;
using System.Windows.Input;
using YouTubeGUI.Commands;
using YouTubeGUI.Models.Snippets;
using YouTubeGUI.Stores;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(YoutubeUser youtubeUser)
        {
            YoutubeUserStore.NotifyInitialRequestFinished += OnNotifyInitialRequestFinished;
            _user = youtubeUser;
            ElementPreparedCommand = new ElementPreparedCommand();
            ElementPreparedCommand.ExecuteLoadContinuation += ExecuteOnLoadContinuation;
        }

        // Properties
        private readonly YoutubeUser _user;
        private HomeSnippet? _homeSnippet;
        private GuideSnippet? _guideSnippet;
        public ElementPreparedCommand ElementPreparedCommand { get; }
        public List<object?>? ContentList => _homeSnippet?.Contents;

        // Functions
        public override void Dispose() => YoutubeUserStore.NotifyInitialRequestFinished -= OnNotifyInitialRequestFinished;

        private void OnNotifyInitialRequestFinished(HomeSnippet? arg1, GuideSnippet? arg2)
        {
            _homeSnippet = arg1;
            _guideSnippet = arg2;
            if (_homeSnippet != null) _homeSnippet.ContentsChanged += OnHomeContentsChanged;
            OnHomeContentsChanged();
        }

        private void OnHomeContentsChanged() => OnPropertyChanged(nameof(ContentList));

        private void ExecuteOnLoadContinuation()
        {
            _homeSnippet?.LoadContinuation(_user);
        }
    }
}