using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using YouTubeGUI.Controls;
using YouTubeGUI.Core;
using YouTubeScrap.Data.Video;

namespace YouTubeGUI.Screens
{
    public class VideoPlayScreen : UserControl
    {
        private MediaPlayer _player;
        private VlcVideoView _view;
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
                _view = this.Find<VlcVideoView>("VideoView");
                if (_view != null)
                {
                    _view.MediaPlayer = _player;
                    Logger.Log("Play media!");
                    Media mediaPlay = new Media(Program.LibVlcManager.LibVlc,
                        vds.StreamingData.MixxedFormats[0].Url, FromType.FromLocation);
                    mediaPlay.AddOption(":network-caching=5000");
                    mediaPlay.AddOption(":clock-jitter=0");
                    mediaPlay.AddOption(":clock-synchro=0");
                    mediaPlay.AddOption(":codec=all");
                    mediaPlay.AddOption($":fps={vds.StreamingData.MixxedFormats[0].Framerate}");
                    _view.MediaPlayer.Play(mediaPlay);
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