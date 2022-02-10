using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Visuals.Media.Imaging;
using YouTubeGUI.Controls;
using YouTubeGUI.Stores;
using YouTubeScrap.Core;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Caches
{
    public static class ImageCache
    {
        private static CacheType Caching { get; set; } = CacheType.Memory;
        private static readonly Dictionary<string, Bitmap> MemoryCache = new Dictionary<string, Bitmap>();
        private static readonly Queue<ImageInfo> DownloadQueue = new Queue<ImageInfo>();
        private static readonly BackgroundWorker ImageDownloader = new BackgroundWorker();
        private static readonly PixelSize MaxPixelSizeWide = new PixelSize(840, 480);
        private static readonly PixelSize MaxPixelSizeSquare = new PixelSize(40, 40);

        static ImageCache()
        {
            ImageDownloader.DoWork += ImageDownloaderOnDoWork;
            ImageDownloader.RunWorkerCompleted += ImageDownloaderOnRunWorkerCompleted;
        }

        //===================================================================================================
        // Public
        //===================================================================================================
        public static void WebImageGetImage(WebImage sender, UrlImage image)
        {
            lock (ImageDownloader)
            {
                ImageInfo imgInfo = new ImageInfo() { Sender = sender, ImageData = image };
                if (ImageDownloader.IsBusy)
                {
                    if (!DownloadQueue.Contains(imgInfo))
                        DownloadQueue.Enqueue(imgInfo);
                }
                else
                    ImageDownloader.RunWorkerAsync(imgInfo);
            }
        }
        public static void WebImageGetImage(WebImage sender, List<UrlImage> images)
        {
            // Implement system to get image based on quality.
            WebImageGetImage(sender, images.First());
        }
        //===================================================================================================
        // Private
        //===================================================================================================
        private static void ImageDownloaderOnRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            lock (ImageDownloader)
            {
                if (e.Result is WorkerResult wr)
                {
                    switch (Caching)
                    {
                        case CacheType.Memory:
                            wr.Sender.Source = wr.Image as Bitmap;
                            break;
                        case CacheType.Disk:
                            Bitmap bm = new Bitmap(wr.Image as string);
                            wr.Sender.Source = bm;
                            break;
                    }
                }
                if (DownloadQueue.Count == 0) return;
                if (!ImageDownloader.IsBusy)
                    ImageDownloader.RunWorkerAsync(DownloadQueue.Dequeue());
            }
        }
        private static void ImageDownloaderOnDoWork(object? sender, DoWorkEventArgs e)
        {
            WorkerResult wr = new WorkerResult();
            if (e.Argument is ImageInfo iiArg)
            {
                wr.Sender = iiArg.Sender;
                if (CheckCache(iiArg.ImageData.Url.Replace('/', '_'), out object? cachedImage))
                {
                    e.Result = new WorkerResult() { Sender = iiArg.Sender, Image = cachedImage};
                    return;
                }
                var imageBytes = GetFromWeb(iiArg.ImageData).Result;
                switch (Caching)
                {
                    case CacheType.Memory:
                    {
                        using MemoryStream memStream = new MemoryStream(imageBytes);
                        Bitmap tempMap = new Bitmap(memStream);
                        if (tempMap.PixelSize.Height > MaxPixelSizeWide.Height)
                            tempMap = tempMap.CreateScaledBitmap(MaxPixelSizeWide, BitmapInterpolationMode.LowQuality);
                        wr.Image = tempMap;
                        break;
                    }
                    case CacheType.Disk:
                        if (!Directory.Exists(SettingsManager.Settings.ImageCachePath))
                            Directory.CreateDirectory(SettingsManager.Settings.ImageCachePath);
                        File.WriteAllBytes(Path.Combine(SettingsManager.Settings.ImageCachePath, iiArg.ImageData.Url.Replace('/', '_')), imageBytes);
                        wr.Image = Path.Combine(SettingsManager.Settings.ImageCachePath, iiArg.ImageData.Url.Replace('/', '_'));
                        break;
                }
                AddCache(iiArg.ImageData.Url.Replace('/', '_'), wr.Image);
                //MemoryCache.Add(iiArg.ImageData.Url, wr.ImageBitmap);
            }
            e.Result = wr;
        }
        private static async Task<byte[]> GetFromWeb(UrlImage img)
        {
            if (img == null) return null;
            var image = img.Url;
            if (image.IsNullEmpty())
            {
                Trace.WriteLine("No image urls found!");
                return null;
            }
            return await YoutubeUserStore.CurrentUser.NetworkHandler.GetDataAsync(image);
        }

        private static bool CheckCache(string url, out object? cachedImage)
        {
            cachedImage = null;
            switch (Caching)
            {
                case CacheType.Memory:
                    if (MemoryCache.ContainsKey(url))
                    {
                        cachedImage = MemoryCache[url];
                        return true;
                    }
                    return false;
                case CacheType.Disk:
                    string path = Path.Combine(SettingsManager.Settings.ImageCachePath, url);
                    if (!File.Exists(path)) return false;
                    cachedImage = path;
                    return true;
                default:
                    return false;
            }
        }

        private static void AddCache(string url, object image)
        {
            switch (Caching)
            {
                case CacheType.Memory:
                    break;
                case CacheType.Disk:
                    break;
                default:
                    break;
            }
        }
    }

    public enum CacheType
    {
        Memory,
        Disk
    }
    public struct ImageInfo
    {
        public UrlImage ImageData;
        public WebImage Sender;
    }

    public struct WorkerResult
    {
        public WebImage Sender;
        public object Image;
    }
}