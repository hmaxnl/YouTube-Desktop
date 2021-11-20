using System.Collections.Generic;
using System.Threading.Tasks;
using YouTubeGUI.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ResponseMetadata Metadata
        {
            get => _metadata;
            set
            {
                _metadata = value;
                Contents = Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs[0].Content.Contents;
                OnPropertyChanged();
            }
        }
        private ResponseMetadata _metadata;
        
        public List<ContentRender> Contents
        {
            get => _contents;
            set
            {
                _contents = value;
                OnPropertyChanged();
            }
        }

        public string TextToGui => "Text from viewmodel!";
        private List<ContentRender> _contents;
        
        private Task<ResponseMetadata> _responseMetadataTask;
        public MainViewModel()
        {
            YoutubeUser user = new YoutubeUser(YoutubeUser.ReadCookies());
            _responseMetadataTask = user.MakeInitRequest();
            Task.Run(RequestAsync);
        }
        
        private async void RequestAsync()
        {
            Logger.Log("Awaiting response...");
            Metadata = await _responseMetadataTask;
        }
    }
}