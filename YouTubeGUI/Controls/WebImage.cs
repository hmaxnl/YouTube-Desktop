using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using YouTubeScrap.Core;
using YouTubeScrap.Data.Extend;
using Image = Avalonia.Controls.Image;

namespace YouTubeGUI.Controls
{
    //TODO: Need to implement a better system to download and set the bitmaps, and with a disk/memory cache option!
    public class WebImage : Image
    {
        public WebImage()
        {
            ImagesProperty.Changed.Where(args => args.IsEffectiveValueChange).Subscribe(args => OnImagesChanged((WebImage)args.Sender, args.NewValue.Value));
        }

        private void OnImagesChanged(WebImage sender, List<Thumbnail> list)
        {
            if (list == null)
            {
                sender.IsVisible = false;
                return;
            }
            Task.Run(async () =>
            {
                var imageBytes = await GetFromWeb(list);
                if (imageBytes == null) return;
                using MemoryStream memStream = new MemoryStream(imageBytes);
                var bitmap = new Bitmap(memStream);
                if (!Dispatcher.UIThread.CheckAccess())
                    Dispatcher.UIThread.Post(() => sender.Source = bitmap);
                else
                    sender.Source = bitmap;
            });
        }

        public static readonly StyledProperty<List<Thumbnail>> ImagesProperty =
            AvaloniaProperty.Register<WebImage, List<Thumbnail>>(nameof(Image));

        public List<Thumbnail> Images
        {
            get => GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }
        
        private async Task<byte[]> GetFromWeb(List<Thumbnail> thumbnails)
        {
            if (thumbnails == null) return null;
            var thumbnail = thumbnails.First().Url;
            if (thumbnail.IsNullEmpty())
            {
                Trace.WriteLine("No image urls found!");
                return null;
            }
            return await Program.MainWindow.CurrentUser.NetworkHandler.GetDataAsync(thumbnail);
        }
    }
}