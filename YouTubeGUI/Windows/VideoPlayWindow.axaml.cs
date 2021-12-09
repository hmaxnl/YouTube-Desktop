using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using YouTubeGUI.Core;
using YouTubeScrap.Data.Video;

namespace YouTubeGUI.Windows
{
    public class VideoPlayWindow : Window
    {
        private MediaPlayer _player;
        private VideoView _view;
        public VideoPlayWindow()
        {
            _player = new MediaPlayer(Program.LibVlcManager.LibVlc);
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            _view = this.Find<VideoView>("VideoView");
            _view.MediaPlayer = _player;
            if (_view.MediaPlayer.IsPlaying)
                _view.MediaPlayer.Stop();
            if (DataContext is VideoDataSnippet vds)
            {
                Logger.Log("DataContext found!");
                if (_view != null)
                {
                    Task.Run(async () =>
                    {
                        Logger.Log("Play media!");
                        Media mediaPlay = new Media(Program.LibVlcManager.LibVlc,
                            vds.StreamingData.MixxedFormats[0].Url, FromType.FromLocation);
                        mediaPlay.AddOption(":network-caching=5000");
                        mediaPlay.AddOption(":clock-jitter=0");
                        mediaPlay.AddOption(":clock-synchro=0");
                        mediaPlay.AddOption(":codec=all");
                        await mediaPlay.Parse(MediaParseOptions.ParseNetwork);
                        _view.MediaPlayer.Play(mediaPlay);
                    });
                }
            }
            base.OnDataContextChanged(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}