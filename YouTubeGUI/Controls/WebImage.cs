using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using YouTubeScrap.Core;
using YouTubeScrap.Data.Extend;
using Image = Avalonia.Controls.Image;

namespace YouTubeGUI.Controls
{
    public class WebImage : Image
    {
        public WebImage()
        {
            bgDownloader = new BackgroundWorker();
            bgDownloader.DoWork += BgDownloaderOnDoWork;
            bgDownloader.RunWorkerCompleted += BgDownloaderOnRunWorkerCompleted;
            ImagesProperty.Changed.Where(args => args.IsEffectiveValueChange).Subscribe(args => OnImagesChanged((WebImage)args.Sender, args.NewValue.Value));
        }

        private void BgDownloaderOnRunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            lock (bgDownloader)
            {
                if (imageQue.Count <= 0) return;
                if (!bgDownloader.IsBusy)
                    bgDownloader.RunWorkerAsync(imageQue.Dequeue());
            }
            if (e.Result is QueImage qImage)
            {
                if (!Dispatcher.UIThread.CheckAccess())
                    Dispatcher.UIThread.Post(() => qImage.Sender.Source = qImage.Bitmap);
                else
                    qImage.Sender.Source = qImage.Bitmap;
            }
        }

        private void OnImagesChanged(WebImage sender, List<Thumbnail> list)
        {
            lock (bgDownloader)
            {
                QueImage qImg = new QueImage() { Sender = sender, List = list};
                if (!bgDownloader.IsBusy)
                    bgDownloader.RunWorkerAsync(qImg);
                else
                    imageQue.Enqueue(qImg);
            }
        }
        private readonly BackgroundWorker bgDownloader;
        private Queue<QueImage> imageQue = new Queue<QueImage>();
        private async void BgDownloaderOnDoWork(object? sender, DoWorkEventArgs e)
        {
            QueImage qimg = new QueImage();
            if (e.Argument is QueImage que)
            {
                qimg = que;
                var imageBytes = await GetFromWeb(que.List);
                if (imageBytes == null) return;
                using MemoryStream memStream = new MemoryStream(imageBytes);
                var bm = new Bitmap(memStream);
                qimg.Bitmap = bm;
                /*if (!Dispatcher.UIThread.CheckAccess())
                    Dispatcher.UIThread.Post(() => que.Sender.Source = bitmap);
                else
                    que.Sender.Source = bitmap;*/
            }
            e.Result = qimg;
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

    struct QueImage
    {
        public WebImage Sender;
        public List<Thumbnail> List;
        public Bitmap Bitmap;
    }
}