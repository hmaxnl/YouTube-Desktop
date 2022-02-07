using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Avalonia;
using YouTubeGUI.Caches;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Controls
{
    public class WebImage : Avalonia.Controls.Image
    {
        public WebImage()
        {
            ImagesProperty.Changed.Where(args => args.IsEffectiveValueChange).Subscribe(args => OnImagesChanged((WebImage)args.Sender, args.NewValue.Value));
        }
        private void OnImagesChanged(WebImage sender, List<UrlImage> list)
        {
            if (list == null) return;
            ImageCache.WebImageGetImage(sender, list);
        }
        public static readonly StyledProperty<List<UrlImage>> ImagesProperty =
            AvaloniaProperty.Register<WebImage, List<UrlImage>>(nameof(UrlImage));

        public List<UrlImage> Images
        {
            get => GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }
    }
}