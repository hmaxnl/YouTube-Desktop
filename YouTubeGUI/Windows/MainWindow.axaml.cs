using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;
using YouTubeGUI.Screens;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Windows
{
    public class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            
            AvaloniaXamlLoader.Load(this);
            DataContext = this;
#if DEBUG
            this.AttachDevTools();
#endif
            SetContent(new LoadingScreen());
        }
        
        // Properties
        public YoutubeUser CurrentUser
        {
            get => _currentUser;
            init
            {
                _currentUser = value;
                Task.Run(async () =>
                {
                    Metadata = await CurrentUser.DataRequestTask;
                }).ContinueWith((t) => SetContent(new HomeScreen()), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
        private readonly YoutubeUser _currentUser;
        public ResponseMetadata? Metadata
        {
            get => _metadata;
            set
            {
                if (value != null)
                    _metadata = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HomePageContentList));
                OnPropertyChanged(nameof(GuideEntries));
            }
        }
        private ResponseMetadata? _metadata;
        
        public List<ContentRender> HomePageContentList
        {
            get
            { //TODO: need to set the list once.
                if (Metadata?.Contents == null) return _homeContentList;
                foreach (var tab in Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs)
                    _homeContentList.AddRange(tab.Content.Contents);
                return _homeContentList;
            }
        }
        private readonly List<ContentRender> _homeContentList = new();

        public List<GuideItemRenderer> GuideEntries
        {
            get
            {
                if (Metadata != null)
                    return Metadata.Items;
                return new List<GuideItemRenderer>();
            }
        }

        public object? ContentView
        {
            get => _contentView;
            set
            {
                if (value != null) // Do not set value but call the property changed to update.
                    _contentView = value;
                OnPropertyChanged();
            }
        }
        private object? _contentView;
        
        public ContentRender SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
        public ContentRender _selectedItem;
        

        public void SetContent(Control element) => ContentView = element;
        
        public new event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}