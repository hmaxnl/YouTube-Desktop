using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using YouTubeScrap.Core;
using YouTubeScrap.Data.Extend;
using Image = Avalonia.Controls.Image;

namespace YouTubeGUI.Core.XamlTools
{
    public class ImageDownloader
    {
        static ImageDownloader()
        {
            ImageListProperty.Changed
                .Where(args => args.IsEffectiveValueChange)
                .Subscribe(args => OnSourceChangedThumbnails((Image)args.Sender, args.NewValue.Value));
        }

        private static async void OnSourceChangedThumbnails(Image sender, List<Thumbnail> thumbnails)
        {
            var bytes = await GetFromWeb(thumbnails);
            if (bytes == null) return;

            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                var bitmap = new Bitmap(memStream);
                sender.Source = bitmap;
            }
        }
        private static async Task<byte[]> GetFromWeb(List<Thumbnail> thumbnails)
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
        
        public static readonly AttachedProperty<List<Thumbnail>> ImageListProperty = AvaloniaProperty.RegisterAttached<Image, List<Thumbnail>>("ImageList", typeof(ImageDownloader));
        
        public static List<Thumbnail> GetImageList(Image element)
        {
            return element.GetValue(ImageListProperty);
        }

        public static void SetImageList(Image element, List<Thumbnail> value)
        {
            element.SetValue(ImageListProperty, value);
        }
    }
}