using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Reflection;
using System.IO;

using Vlc.DotNet.Wpf;

using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using YouTube_Desktop.Core;
using YouTube_Desktop.Core.Google.YouTube;
using YouTube_Desktop.Core.Models;
using YouTube_Desktop.Page;

using YouTubeScrap;

namespace YouTube_Desktop.ViewModel
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private YouTubeScrapService P_youtubeScrapService;
        // Ctor
        public MainWindowModel()
        {
            P_youtubeScrapService = new YouTubeScrapService();
            PlaylistItem pi = new PlaylistItem();
            SearchResult sr = new SearchResult();
        }

        // Publics
        public event PropertyChangedEventHandler PropertyChanged;

        // Binding variables
        public object ContentControlView
        {
            get => _contentControlView;
            set
            {
                _contentControlView = value;
                OnPropertyChanged(nameof(ContentControlView));
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public PlaylistItemListResponse PLIR
        {
            get => _playListResponse;
            set
            {
                _playListResponse = value;
            }
        }
        SearchListResponse _searchResponse = new SearchListResponse();
        public SearchListResponse SearchResponse
        {
            get => _searchResponse;
            set { _searchResponse = value; }
        }
        List<SearchResult> _pli = new List<SearchResult>();
        public List<SearchResult> SearchResultItems
        {
            get => (List<SearchResult>)SearchResponse.Items;
        }
        SearchResult _selectedSearchResult = new SearchResult();
        public SearchResult SelectedSearchResult
        {
            get => _selectedSearchResult;
            set { _selectedSearchResult = value; }
        }

        // Commands
        public ICommand ToggleMenuCommand
        {
            get => new CommandHandler(() => TestVoidInternal(), () => CanExecute); // Need to set back to ToggleMenu()
        }
        public ICommand HomeButtonCommand
        {
            get => new CommandHandler(() => SetView(new VideoPage()), () => CanExecute);
        }
        public ICommand SearchCommand
        {
            get => new CommandHandler(() => SearchFromInput(), () => CanExecute);
        }
        public ICommand SearchListLeftMouseClickCommand
        {
            get => new CommandHandler(() => SearchListLeftMouseUp(), () => CanExecute);
        }
        public bool CanExecute
        {
            get => Application.Current.Dispatcher.CheckAccess();
        }

        // Command/Event functions
        public void SearchFromInput()
        {
            SetView(new HomePage());

            YTSearch yts = new YTSearch();
            List<SearchResult> results = new List<SearchResult>();

            Task<SearchListResponse> searchTask = Task<SearchListResponse>.Factory.StartNew(() => yts.SearchYTAsync(SearchText).Result);
            SearchResponse = searchTask.Result;
        }
        /// <summary>
        /// Test function for calling and testing code Only (Developer(s) only.)
        /// </summary>
        internal void TestVoidInternal()
        {
            //HttpManager _httpManager = new HttpManager();
            //YouTubeRequestParser parser = new YouTubeRequestParser();
            //// Test urls: 9rIJK0VQO_8 = working || 5oKu_tDP_2U = removed || Le7cJ_SItO0 = not playable in embed & cipher || 4h0XQiNxnfk = Vevo content || CK0BD2lN0vE = Live content & cipher
            //Task<RequestResponse> taskResponse = Task.Run(async () => await _httpManager.GetVideoResponseAsync("n6ku44S8aVc"));
            //RequestResponse response = taskResponse.Result;

            //Task<YouTubeRequestJsonParseRaw> taskParse = Task.Run(async () => await parser.GetJsonPlayerParseFromResponse(response));
            //response.rawJsonData = taskParse.Result;
            //Task<VideoInfoSnippet> videoInfoSnippetTask = Task.Run(() => parser.GetVideoProperties(response));
            //VideoInfoSnippet videoInfo = videoInfoSnippetTask.Result;
            //Task<string> playerDatTask = Task.Run(() => _httpManager.GetCipherFunctionsAsync(videoInfo.PlayerData));
            //string pData = playerDatTask.Result;
            P_youtubeScrapService.TestCall();
        }

        public void ToggleMenu()
        {
            // Whats this?
            //ResourceDictionary rdict = new ResourceDictionary();
            //rdict.Source = new Uri("pack://application:,,,/Styles/DefaultStyle.xaml", UriKind.Absolute);
            //Application.Current.Resources["AppBackgroundColor"] = Color.FromRgb(0, 0, 100);
            //Application.Current.Resources.MergedDictionaries.Clear();
            //Application.Current.Resources.MergedDictionaries.Add(rdict);

            if (MainWindowElement().FindName("AppMenu") is UserControl _menuControl)
            {
                if (_menuControl.Dispatcher.CheckAccess())// Check acces if called from other thread!
                {
                    if (_menuControl.Visibility == Visibility.Visible)
                        _menuControl.Visibility = Visibility.Collapsed;
                    else
                        _menuControl.Visibility = Visibility.Visible;
                }
                else
                {
                    //TODO: Needs a delagate and make a invoke!
                }
            }
        }
        private void SearchListLeftMouseUp()
        {
            YTVideo video = new YTVideo();
            //Video vid;
            
            //SetView(new VideoPage());
            //vid = video.GetVideoInfo(SelectedSearchResult.Id.VideoId);
            if (MainWindowElement().FindName("MainWindowContent") is ContentControl _contentControl)
            {
                VideoPage variable = (VideoPage)_contentControl.Content;

                var currentAssembly = Assembly.GetEntryAssembly();
                var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
                var libDir = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

                var mediaOpt = new[]
                {
                    ""
                };

                if (variable.FindName("vlcPlayer") is VlcControl _vlcPlayer)
                {
                    _vlcPlayer.SourceProvider.CreatePlayer(libDir);
                    _vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri(@"https://r2---sn-5hne6n7z.googlevideo.com/videoplayback?expire=1607566721&ei=ITHRX7b0J8WrgQek_4SYAQ&ip=2a02%3Aa451%3A49e8%3A1%3A50cf%3A4d85%3A79d4%3A46db&id=o-AJpsQIp5xr-zSjmur7mRxBK58vGoxj4XYsaq3xrDxSV9&itag=18&source=youtube&requiressl=yes&mh=fP&mm=31%2C29&mn=sn-5hne6n7z%2Csn-32o-5hnl&ms=au%2Crdu&mv=m&mvi=2&pl=43&initcwndbps=1372500&vprv=1&mime=video%2Fmp4&ns=dMo2OOU1fjKCq_y1Q80tx6YF&gir=yes&clen=32813317&ratebypass=yes&dur=733.100&lmt=1551638925130024&mt=1607544768&fvip=2&c=WEB&txp=5531432&n=icSUvsi9WgEkohCy&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cvprv%2Cmime%2Cns%2Cgir%2Cclen%2Cratebypass%2Cdur%2Clmt&sig=AOq0QJ8wRAIgaMyog5JxYa74XPeNZ0DHP6LFFn1PdWdUXlTbLQSHSUcCICGUvr4nlx0dXH-0j99D8tWtK89cG0t-fU_eQPxw_l6j&lsparams=mh%2Cmm%2Cmn%2Cms%2Cmv%2Cmvi%2Cpl%2Cinitcwndbps&lsig=AG3C_xAwRAIgM_qqIy1GULkKInycNczMaskLZLq0SwJwGvGGqsVGUnoCICXL3sfpUF9yiW2sLzDjkNbiGqlbAIvpAgGnBaQJw2DR"));

                    //_vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri(string.Format("http://www.youtube.com/watch?v={0}", vid.Id))); http://www.youtube.com/watch?v=-Rf56TeiEBs
                }
            }
            //if (SelectedSearchResult.Id.Kind == "youtube#video")
            //{
            //    SetView(new VideoPage());
            //    vid = video.GetVideoInfo(SelectedSearchResult.Id.VideoId);
            //    if (MainWindowElement().FindName("vlcPlayer") is VlcControl _vlcPlayer)
            //    {
            //        _vlcPlayer.SourceProvider.CreatePlayer(new System.IO.DirectoryInfo(@"D:\Programming\GitHub\YouTube-Desktop\YouTube Desktop\3rp\vlc-3.0.9.2"));
            //        _vlcPlayer.SourceProvider.MediaPlayer.Play(@"E:\Video's\HD Epic Sax Gandalf.mp4");
            //        //_vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri(string.Format("http://www.youtube.com/watch?v={0}", vid.Id)));
            //    }
            //}
            //else if (SelectedSearchResult.Id.Kind == "youtube#playlist")
            //{

            //}
            //else if (SelectedSearchResult.Id.Kind == "youtube#channel")
            //{

            //}
            //else { }// Not found throw exception!
        }
        private string GetEmbeddedUrl(string videoUrl)
        {
            return null;
        }
        public void SetView(object page) // TESTING!!!
        {
            ContentControlView = page;
            //Pli.Add(new SearchResult()
            //{
            //    ETag = "Null",
            //    Id = new ResourceId() { Kind = "youtube#playlist" },
            //    Snippet =
            //        new SearchResultSnippet()
            //        {
            //            ChannelTitle = $"Channel name",
            //            Title = "Playlist",
            //            PublishedAt = "A Date.",
            //            Thumbnails = new ThumbnailDetails()
            //            {
            //                Default__ = new Thumbnail()
            //                {
            //                    Url = "https://roadtovrlive-5ea0.kxcdn.com/wp-content/uploads/2015/03/youtube-logo2.jpg"
            //                }
            //            }
            //        }
            //});
            //Pli.Add(new SearchResult()
            //{
            //    ETag = "Null",
            //    Id = new ResourceId() { Kind = "youtube#video" },
            //    Snippet =
            //        new SearchResultSnippet()
            //        {
            //            ChannelTitle = $"Channel name",
            //            Title = "Video",
            //            PublishedAt = "A Date.",
            //            Thumbnails = new ThumbnailDetails()
            //            {
            //                Default__ = new Thumbnail()
            //                {
            //                    Url = "https://roadtovrlive-5ea0.kxcdn.com/wp-content/uploads/2015/03/youtube-logo2.jpg"
            //                }
            //            }
            //        }
            //});
            //Pli.Add(new SearchResult()
            //{
            //    ETag = "Null",
            //    Id = new ResourceId() { Kind = "youtube#channel" },
            //    Snippet =
            //        new SearchResultSnippet()
            //        {
            //            ChannelTitle = $"Channel name",
            //            Title = "Channel",
            //            PublishedAt = "A Date.",
            //            Description = "Description",
            //            Thumbnails = new ThumbnailDetails()
            //            {
            //                Default__ = new Thumbnail()
            //                {
            //                    Url = "https://roadtovrlive-5ea0.kxcdn.com/wp-content/uploads/2015/03/youtube-logo2.jpg"
            //                }
            //            }
            //        }
            //});
        }

        // Privates
        private static object _contentControlView;
        private string _searchText;

        private static PlaylistItemListResponse _playListResponse;

        // Private voids
        private void OnPropertyChanged(string PropName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropName));
        }
        private FrameworkElement MainWindowElement()
        {
            Window _selectWindow = Application.Current.MainWindow as Window;
            return _selectWindow.Content as FrameworkElement;
        }
    }
}