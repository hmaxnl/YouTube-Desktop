using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis;
using YouTubeGUI.ContentWindows;
using YouTubeGUI.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Windows
{
    public class MainWindow : Window
    {
        /* Controls */
        private readonly ContentControl? _contentControlMain;

        public ResponseMetadata Metadata => _metadata;
        public List<ContentRender> Contents => _contents;
        private ResponseMetadata _metadata;
        private List<ContentRender> _contents;
        public MainWindow()
        {
            _contents = new List<ContentRender>();
            Contents.Add(new ContentRender()
            {
                RichItem = new RichItemRenderer()
                {
                    RichItemContent = new RichItemContent()
                    {
                        VideoRenderer = new VideoRenderer()
                        {
                            VideoId = "Video from C#"
                        }
                    }
                }
            });
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
            _contentControlMain = this.Find<ContentControl>("ContentControlMain");
            ListBox lb = this.Find<ListBox>("lsResults");

            YoutubeUser user = new YoutubeUser(YoutubeUser.ReadCookies());
            //Task.Run(() => user.MakeInitRequest(ref _metadata));
            Task init = new Task(() => user.MakeInitRequest(ref _metadata));
            init.Start();
            init.Wait();

            if (lb != null)
            {
                Logger.Log("Linking items");
                lb.Items = Metadata.Contents.TwoColumnBrowseResultsRenderer.Tabs[0].Content.Contents;
            }
            /*if (_contentControlMain != null)
                _contentControlMain.Content = new HomeContent();*/
        }
        
    }
}