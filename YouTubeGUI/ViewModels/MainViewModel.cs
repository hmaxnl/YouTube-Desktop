using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using YouTubeGUI.Core;
using YouTubeGUI.Screens;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;
using YouTubeScrap.Handlers;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel
    {
        public YoutubeUser CurrentUser;
        
        public MainViewModel()
        {
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
        

        public void SetContent(Control element) => ContentView = element;
    }
}