using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeGUI.Core;
using YouTubeGUI.Screens;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Handlers;
using YouTubeScrap.Util.JSON;

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
            set => _selectedItem = value;
        }

        public YoutubeUser CurrentUser;
        private ResponseMetadata? _metadata;
        private object? _contentView;
        public ContentRender _selectedItem;
        
        public MainViewModel()
        {
            CurrentUser = new YoutubeUser(YoutubeUser.ReadCookies());
            SetContent(new LoadingScreen());
            
            Task.Run(async () =>
            {
                Logger.Log("Getting init data...");
                Metadata = await CurrentUser.MakeInitRequest();
            }).ContinueWith((t) =>
            {
                Logger.Log("Getting guide data...");
                ApiRequest testReq = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Guide, CurrentUser);
                HttpResponse resp = CurrentUser.NetworkHandler.MakeApiRequestAsync(testReq).Result;
                JObject? guideJson =
                    JsonConvert.DeserializeObject<JObject>(resp.ResponseString, new JsonDeserializeConverter());
                ResponseMetadata rMeta = JsonConvert.DeserializeObject<ResponseMetadata>(guideJson.ToString());
            });

            /*Task.Run(async () =>
            {
                Logger.Log("Getting data...");
                Metadata = await CurrentUser.MakeInitRequest();
            }).ContinueWith((t) => { SetContent(new HomeScreen()); }, TaskScheduler.FromCurrentSynchronizationContext());*/
        }

        public void SetContent(Control element) => AskDispatcher(() => ContentView = element);
    }
}