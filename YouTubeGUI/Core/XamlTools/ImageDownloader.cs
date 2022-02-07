using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using YouTubeScrap.Core;
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
        private static async void OnSourceChangedThumbnails(Image sender, List<YouTubeScrap.Data.Extend.UrlImage> thumbnails)
        {
            var bytes = await GetFromWeb(thumbnails);
            if (bytes == null) return;

            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                var bitmap = new Bitmap(memStream);
                sender.Source = bitmap;
            }
        }
        private static async Task<byte[]> GetFromWeb(List<YouTubeScrap.Data.Extend.UrlImage> thumbnails)
        {
            if (thumbnails == null) return null;
            var thumbnail = thumbnails.First().Url;
            if (thumbnail.IsNullEmpty())
            {
                Trace.WriteLine("No image urls found!");
                return null;
            }

            throw new NotImplementedException();
            //return await Program.MainWindow.CurrentUser.NetworkHandler.GetDataAsync(thumbnail);
        }
        
        public static readonly AttachedProperty<List<YouTubeScrap.Data.Extend.UrlImage>> ImageListProperty = AvaloniaProperty.RegisterAttached<Image, List<YouTubeScrap.Data.Extend.UrlImage>>("ImageList", typeof(ImageDownloader));
        
        public static List<YouTubeScrap.Data.Extend.UrlImage> GetImageList(Image element)
        {
            return element.GetValue(ImageListProperty);
        }

        public static void SetImageList(Image element, List<YouTubeScrap.Data.Extend.UrlImage> value)
        {
            element.SetValue(ImageListProperty, value);
        }
    }
}