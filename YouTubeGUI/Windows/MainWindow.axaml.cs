using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;
using YouTubeGUI.Core;
using YouTubeGUI.Screens;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.Windows
{
    public class MainWindow : Window, INotifyPropertyChanged
    {
        public YoutubeUser CurrentUser;
        public ContentControl ScreenViewer => this.Find<ContentControl>("ContentControlMain");
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = this;
            TestList.Add("Test val");
#if DEBUG
            this.AttachDevTools();
#endif
            
            CurrentUser = new YoutubeUser(YoutubeUser.ReadCookies());
            SetContent(new LoadingScreen());
            
            Task.Run(async () =>
            {
                Logger.Log("Getting init data...");
                HomeMetadata = await CurrentUser.MakeInitRequest();
            }).ContinueWith((t) => { SetContent(new HomeScreen()); }, TaskScheduler.FromCurrentSynchronizationContext()).ContinueWith(async (t) =>
            {
                Logger.Log("Getting guide data...");
                GuideMetadata = await CurrentUser.MakeRequestAsync(ApiRequestType.Guide);
            });
        }
        
        // Properties
        public ResponseMetadata? HomeMetadata
        {
            get => _homeMetadata;
            set
            {
                if (value != null)
                    _homeMetadata = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HomePageContentList));
            }
        }
        private ResponseMetadata? _homeMetadata;
        
        public List<ContentRender> HomePageContentList
        {
            get
            { //TODO: need to set the list once.
                if (HomeMetadata?.Contents == null) return _homeContentList;
                foreach (var tab in HomeMetadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
                    _homeContentList.AddRange(tab.Content.Contents);
                return _homeContentList;
            }
        }
        private readonly List<ContentRender> _homeContentList = new();
        
        public ResponseMetadata? GuideMetadata
        {
            get => _guideMetadata;
            set
            {
                if (value != null)
                    _guideMetadata = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GuideEntries));
            }
        }
        private ResponseMetadata? _guideMetadata;

        public List<GuideItemRenderer> GuideEntries
        {
            get
            {
                if (GuideMetadata != null)
                    return GuideMetadata.Items;
                return new List<GuideItemRenderer>();
            }
        }

        public List<string> TestList => new List<string>();

        public object? ContentView
        {
            get => _contentView;
            set
            {
                if (value != null) // Do not set value but call the property changed to update.
                    _contentView = value;
            }
        }
        private object? _contentView;
        
        public ContentRender SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
        public ContentRender _selectedItem;
        

        public void SetContent(Control element) => ScreenViewer.Content = element;
        
        public new event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}