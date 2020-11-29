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
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using YouTube_Desktop.Control;
using YouTube_Desktop.Core;
using YouTube_Desktop.Page;
using YouTube_Desktop.Views;

namespace YouTube_Desktop.ViewModel
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        // Ctor
        public MainWindowModel()
        {
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
        List<SearchResult> _pli = new List<SearchResult>();
        public List<SearchResult> Pli
        {
            get => _pli;
            set
            {
                _pli = value;
                OnPropertyChanged(nameof(Pli));
            }
        }
        // Commands
        public ICommand ToggleMenuCommand
        {
            get => new CommandHandler(() => ToggleMenu(), () => CanExecute);
        }
        public ICommand HomeButtonCommand
        {
            get => new CommandHandler(() => SetView(), () => CanExecute);
        }
        public bool CanExecute
        {
            get => Application.Current.Dispatcher.CheckAccess();
        }

        // Command functions
        public void ToggleMenu()
        {
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

        public void SetView() // TESTING!!!
        {
            ContentControlView = new HomePage();
            Pli.Add(new SearchResult()
            {
                ETag = "Null",
                Id = new ResourceId() { Kind = "youtube#playlist" },
                Snippet =
                    new SearchResultSnippet()
                    {
                        ChannelTitle = $"Channel name",
                        Title = "Playlist",
                        PublishedAt = "A Date.",
                        Thumbnails = new ThumbnailDetails()
                        {
                            Default__ = new Thumbnail()
                            {
                                Url = "https://roadtovrlive-5ea0.kxcdn.com/wp-content/uploads/2015/03/youtube-logo2.jpg"
                            }
                        }
                    }
            });
            Pli.Add(new SearchResult()
            {
                ETag = "Null",
                Id = new ResourceId() { Kind = "youtube#video" },
                Snippet =
                    new SearchResultSnippet()
                    {
                        ChannelTitle = $"Channel name",
                        Title = "Video",
                        PublishedAt = "A Date.",
                        Thumbnails = new ThumbnailDetails()
                        {
                            Default__ = new Thumbnail()
                            {
                                Url = "https://roadtovrlive-5ea0.kxcdn.com/wp-content/uploads/2015/03/youtube-logo2.jpg"
                            }
                        }
                    }
            });
            Pli.Add(new SearchResult()
            {
                ETag = "Null",
                Id = new ResourceId() { Kind = "youtube#channel" },
                Snippet =
                    new SearchResultSnippet()
                    {
                        ChannelTitle = $"Channel name",
                        Title = "Channel",
                        PublishedAt = "A Date.",
                        Description = "Description",
                        Thumbnails = new ThumbnailDetails()
                        {
                            Default__ = new Thumbnail()
                            {
                                Url = "https://roadtovrlive-5ea0.kxcdn.com/wp-content/uploads/2015/03/youtube-logo2.jpg"
                            }
                        }
                    }
            });
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