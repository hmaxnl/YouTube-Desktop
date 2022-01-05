using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Media.Imaging;
using YouTubeGUI.Caches;
using YouTubeScrap.Data.Extend;
using Image = Avalonia.Controls.Image;

namespace YouTubeGUI.Controls
{
    public class WebImage : Image
    {
        public WebImage()
        {
            ImagesProperty.Changed.Where(args => args.IsEffectiveValueChange).Subscribe(args => OnImagesChanged((WebImage)args.Sender, args.NewValue.Value));
            Source = new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), "Images/loading_image.jpg"));
        }
        private void OnImagesChanged(WebImage sender, List<Thumbnail> list)
        {
            if (list == null) return;
            ImageCache.WebImageGetImage(sender, list.First());
        }
        public static readonly StyledProperty<List<Thumbnail>> ImagesProperty =
            AvaloniaProperty.Register<WebImage, List<Thumbnail>>(nameof(Image));

        public List<Thumbnail> Images
        {
            get => GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }
    }
}