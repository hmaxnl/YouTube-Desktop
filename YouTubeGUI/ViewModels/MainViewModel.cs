using System.Threading.Tasks;
using Avalonia.Controls;
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
                if (value != null)
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
                if (value != null) // Do not set value but call the property changed to update.
                    _contentView = value;
                OnPropertyChanged();
            }
        }

        public ContentRender SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value.RichItem != null)
                    _selectedItem = value;
                if (value.RichSection?.RichSectionContent.RichShelfRenderer != null)
                    _selectedItem = value.RichSection.RichSectionContent.RichShelfRenderer.SelectedItem;
            }
        }

        public YoutubeUser CurrentUser;
        private ResponseMetadata? _metadata;
        private object? _contentView;
        public ContentRender _selectedItem;
        
        public MainViewModel()
        {
            CurrentUser = new YoutubeUser();
            SetContent(new LoadingScreen());
            
            Task.Run(async () =>
            {
                Logger.Log("Getting data...");
                Metadata = await CurrentUser.MakeInitRequest();
            }).ContinueWith((t) => { SetContent(new HomeScreen()); }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void SetContent(Control element) => AskDispatcher(() => ContentView = element);
    }
}