using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        List<PlaylistItem> _pli = new List<PlaylistItem>();
        public List<PlaylistItem> pli
        {
            get => _pli;
            set
            {
                _pli = value;
                OnPropertyChanged(nameof(pli));
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

        public void SetView()
        {
            ContentControlView = new HomePage();
            pli.Add(new PlaylistItem() { ETag = "Null", Snippet = new PlaylistItemSnippet() { ChannelId = "Channel" } });
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