using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        // Properties
        public static readonly StyledProperty<List<UrlImage>> ImagesProperty =
            AvaloniaProperty.Register<WebImage, List<UrlImage>>(nameof(UrlImage));
        public List<UrlImage> Images
        {
            get => GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

        public static readonly StyledProperty<ImageSize> ImageSizeProperty =
            AvaloniaProperty.Register<WebImage, ImageSize>(nameof(ImageSize));
        public ImageSize ImageSize
        {
            get => GetValue(ImageSizeProperty);
            set => SetValue(ImageSizeProperty, value);
        }
        //
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        private void OnImagesChanged(WebImage sender, List<UrlImage> list)
        {
            if (list == null || list.Count == 0)
            {
                sender.Source = null;
                return;
            }
            ImageCache.WebImageGetImage(sender, list);
        }
    }

    public enum ImageSize
    {
        Default,
        Wide,
        Square
    }
}