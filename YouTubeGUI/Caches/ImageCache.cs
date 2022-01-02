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
        private static Dictionary<string, Bitmap> _memoryCache = new Dictionary<string, Bitmap>();
        private static Queue<ImageInfo> _downloadQueue = new Queue<ImageInfo>();
        private static BackgroundWorker _bgDownloader = new BackgroundWorker();

        static ImageCache()
        {
            _bgDownloader.DoWork += BgDownloaderOnDoWork;
            _bgDownloader.RunWorkerCompleted += BgDownloaderOnRunWorkerCompleted;
        }

        private static void BgDownloaderOnRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            lock (_bgDownloader)
            {
                if (e.Result is WorkerResult wr)
                    wr.Sender.Source = wr.ImageBitmap;
                if (_downloadQueue.Count == 0) return;
                if (!_bgDownloader.IsBusy)
                    _bgDownloader.RunWorkerAsync(_downloadQueue.Dequeue());
            }
        }

        private static void BgDownloaderOnDoWork(object? sender, DoWorkEventArgs e)
        {
            WorkerResult wr = new WorkerResult();
            if (e.Argument is ImageInfo iiArg)
            {
                wr.Sender = iiArg.Sender;
                if (_memoryCache.ContainsKey(iiArg.ImageData.Url))
                {
                    e.Result = new WorkerResult() { Sender = iiArg.Sender, ImageBitmap = _memoryCache[iiArg.ImageData.Url]};
                    return;
                }
                var imageBytes = GetFromWeb(iiArg.ImageData).Result;
                using MemoryStream memStream = new MemoryStream(imageBytes);
                wr.ImageBitmap = new Bitmap(memStream);
                _memoryCache.Add(iiArg.ImageData.Url, wr.ImageBitmap);
            }
            e.Result = wr;
        }

        public static void GetImage(WebImage sender, Image image)
        {
            lock (_bgDownloader)
            {
                ImageInfo imgInfo = new ImageInfo() { Sender = sender, ImageData = image };
                if (_bgDownloader.IsBusy)
                {
                    if (!_downloadQueue.Contains(imgInfo))
                        _downloadQueue.Enqueue(imgInfo);
                }
                else
                    _bgDownloader.RunWorkerAsync(imgInfo);
            }
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