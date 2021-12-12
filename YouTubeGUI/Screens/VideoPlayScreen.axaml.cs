using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Shared;
using YouTubeGUI.Controls;

namespace YouTubeGUI.Screens
{
    public class VideoPlayScreen : UserControl
    {
        public MediaPlayer Player
        {
            get => _player;
            set
            {
                _player = value;
                _view.MediaPlayer = _player;
                _view.MediaPlayer.Play();
            }
        }
        private MediaPlayer _player;
        private VlcVideoView _view => this.Find<VlcVideoView>("VideoView");
        public VideoPlayScreen()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}