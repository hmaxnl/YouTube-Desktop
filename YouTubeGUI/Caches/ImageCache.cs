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
        private static readonly Dictionary<string, Bitmap> MemoryCache = new Dictionary<string, Bitmap>();
        private static readonly Queue<ImageInfo> DownloadQueue = new Queue<ImageInfo>();
        private static readonly BackgroundWorker BgDownloader = new BackgroundWorker();
        private static readonly PixelSize MaxPixelSize = new PixelSize(840, 480);

        static ImageCache()
        {
            BgDownloader.DoWork += BgDownloaderOnDoWork;
            BgDownloader.RunWorkerCompleted += BgDownloaderOnRunWorkerCompleted;
        }

        private static void BgDownloaderOnRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            lock (BgDownloader)
            {
                if (e.Result is WorkerResult wr)
                    wr.Sender.Source = wr.ImageBitmap;
                if (DownloadQueue.Count == 0) return;
                if (!BgDownloader.IsBusy)
                    BgDownloader.RunWorkerAsync(DownloadQueue.Dequeue());
            }
        }

        private static void BgDownloaderOnDoWork(object? sender, DoWorkEventArgs e)
        {
            WorkerResult wr = new WorkerResult();
            if (e.Argument is ImageInfo iiArg)
            {
                wr.Sender = iiArg.Sender;
                if (MemoryCache.ContainsKey(iiArg.ImageData.Url))
                {
                    e.Result = new WorkerResult() { Sender = iiArg.Sender, ImageBitmap = MemoryCache[iiArg.ImageData.Url]};
                    return;
                }
                var imageBytes = GetFromWeb(iiArg.ImageData).Result;
                using MemoryStream memStream = new MemoryStream(imageBytes);
                Bitmap tempMap = new Bitmap(memStream);
                if (tempMap.PixelSize.Height > MaxPixelSize.Height)
                    tempMap = tempMap.CreateScaledBitmap(MaxPixelSize, BitmapInterpolationMode.LowQuality);
                wr.ImageBitmap = tempMap;
                MemoryCache.Add(iiArg.ImageData.Url, wr.ImageBitmap);
            }
            e.Result = wr;
        }

        public static void WebImageGetImage(WebImage sender, UrlImage image)
        {
            lock (BgDownloader)
            {
                ImageInfo imgInfo = new ImageInfo() { Sender = sender, ImageData = image };
                if (BgDownloader.IsBusy)
                {
                    if (!DownloadQueue.Contains(imgInfo))
                        DownloadQueue.Enqueue(imgInfo);
                }
                else
                    BgDownloader.RunWorkerAsync(imgInfo);
            }
        }

        public static void WebImageGetImage(WebImage sender, List<UrlImage> images)
        {
            // Implement system to get image based on quality.
            WebImageGetImage(sender, images.First());
        }
        
        private static async Task<byte[]> GetFromWeb(UrlImage img)
        {
            if (img == null) return null;
            var thumbnail = img.Url;
            //Logger.Log("Downloading image!", LogType.Debug);
            if (thumbnail.IsNullEmpty())
            {
                Trace.WriteLine("No image urls found!");
                return null;
            }
            return await YoutubeUserStore.CurrentUser.NetworkHandler.GetDataAsync(thumbnail);
        }
    }

    public struct ImageInfo
    {
        public UrlImage ImageData;
        public WebImage Sender;
    }

    public struct WorkerResult
    {
        public WebImage Sender;
        public Bitmap ImageBitmap;
    }
}