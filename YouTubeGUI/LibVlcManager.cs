using System;
using System.Collections;
using System.Collections.Generic;
using LibVLCSharp.Shared;
using YouTubeGUI.Core;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI
{
    public class LibVlcManager
    {
        public LibVLC LibVlc
        {
            get => _libVlc;
            set => _libVlc = value;
        }

        public bool IsInitialized => _libVlc != null;

        private LibVLC _libVlc;
        
        public LibVlcManager()
        {
            Logger.Log("Initializing LibVLC...");
            LibVLCSharp.Shared.Core.Initialize();
            // setting options does not work for some reason!
            /*List<string> libVlcOptions = new List<string>();
            libVlcOptions.Add("--network-caching=8000");
            libVlcOptions.Add("--directx-use-sysmem");
            //libVlcOptions.Add("-vvv"); // Verbose
            libVlcOptions.Add("--no-drop-late-frames");
            libVlcOptions.Add("--no-skip-frames");
            libVlcOptions.Add("--avcodec-skip-frame");
            libVlcOptions.Add("--avcodec-hw=any");*/
            LibVlc = new LibVLC();
            LibVlc.Log += LibVlcOnLog;
        }

        private void LibVlcOnLog(object? sender, LogEventArgs e)
        {
            switch (e.Level)
            {
                case LogLevel.Debug:
                    Logger.LogExtend(e.Message, LogType.Debug, e.Module);
                    break;
                case LogLevel.Notice:
                    Logger.LogExtend(e.Message, LogType.Info, e.Module);
                    break;
                case LogLevel.Warning:
                    Logger.LogExtend(e.Message, LogType.Warning, e.Module);
                    break;
                case LogLevel.Error:
                    Logger.LogExtend(e.Message, LogType.Error, e.Module);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}