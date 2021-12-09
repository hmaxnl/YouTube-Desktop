using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using YouTubeGUI.Core;
using YouTubeScrap.Data.Video;

namespace YouTubeGUI.Screens
{
    public class VideoPlayScreen : UserControl
    {
        private MediaPlayer _player;
        private VideoView _view;
        public VideoPlayScreen()
        {
            _player = new MediaPlayer(Program.LibVlcManager.LibVlc);
            InitializeComponent();
        }

        protected override void OnInitialized()
        {
            if (DataContext is VideoDataSnippet vds)
            {
                Logger.Log("DataContext found!");
                _view = this.Find<VideoView>("VideoView");
                if (_view != null)
                {
                    _view.MediaPlayer = _player;
                    Logger.Log("Play media!");
                    _view.MediaPlayer.Play(new Media(Program.LibVlcManager.LibVlc,
                        vds.StreamingData.MixxedFormats[0].Url, FromType.FromLocation));
                }
            }
            base.OnInitialized();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}