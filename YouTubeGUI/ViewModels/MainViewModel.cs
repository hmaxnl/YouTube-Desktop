using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using YouTubeGUI.Core;
using YouTubeGUI.Screens;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ResponseMetadata? Metadata
        {
            get => _metadata;
            set
            {
                _metadata = value;
                OnPropertyChanged();
            }
        }
        // Main content that is on the screen.
        public object? ContentView
        {
            get => _contentView;
            set
            {
                _contentView = value;
                OnPropertyChanged();
            }
        }

        public ContentRender SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public YoutubeUser CurrentUser;
        private ResponseMetadata? _metadata;
        private object? _contentView;
        public ContentRender _selectedItem;
        
        public MainViewModel()
        {
            // Setup settings and choose which already logged in user to load.
            CurrentUser = new YoutubeUser(YoutubeUser.ReadCookies());
            ContentView = new LoadingScreen();
            
            Task.Run(() =>
            {
                Logger.Log("Getting data...");
                Metadata = CurrentUser.MakeInitRequest();
                AskDispatcher(() =>
                {
                    ContentView = new HomeScreen();
                });
            });
        }

        // Used for UI things.
        private void AskDispatcher(Action a)
        {
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.Post(a);
            }
            else
                a();
        }
    }
}