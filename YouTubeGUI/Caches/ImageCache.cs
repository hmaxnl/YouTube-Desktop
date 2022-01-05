using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using YouTubeGUI.Controls;
using YouTubeGUI.Core;
using YouTubeGUI.Stores;
using YouTubeScrap.Core;
using Image = YouTubeScrap.Data.Extend.Image;

namespace YouTubeGUI.Caches
{
    public static class ImageCache
    {
        private static readonly Dictionary<string, Bitmap> MemoryCache = new Dictionary<string, Bitmap>();
        private static readonly Queue<ImageInfo> DownloadQueue = new Queue<ImageInfo>();
        private static readonly BackgroundWorker BgDownloader = new BackgroundWorker();

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
                wr.ImageBitmap = new Bitmap(memStream);
                MemoryCache.Add(iiArg.ImageData.Url, wr.ImageBitmap);
            }
            e.Result = wr;
        }

        public static void WebImageGetImage(WebImage sender, Image image)
        {
            /*lock (BgDownloader)
            {
                ImageInfo imgInfo = new ImageInfo() { Sender = sender, ImageData = image };
                if (BgDownloader.IsBusy)
                {
                    if (!DownloadQueue.Contains(imgInfo))
                        DownloadQueue.Enqueue(imgInfo);
                }
                else
                    BgDownloader.RunWorkerAsync(imgInfo);
            }*/
        }
        
        private static async Task<byte[]> GetFromWeb(Image img)
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
        public Image ImageData;
        public WebImage Sender;
    }

    public struct WorkerResult
    {
        public WebImage Sender;
        public Bitmap ImageBitmap;
    }
}