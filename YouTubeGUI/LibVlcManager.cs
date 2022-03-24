using System;
using System.Collections;
using System.Collections.Generic;
using LibVLCSharp.Shared;
using YouTubeGUI.Core;
using YouTubeScrap.Data.Extend;
using LogLevel = LibVLCSharp.Shared.LogLevel;

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
            List<string> libVlcOptions = new List<string>();
            libVlcOptions.Add("--network-caching");
            libVlcOptions.Add("--directx-use-sysmem");
            libVlcOptions.Add("--adaptive-logic=highest");
            LibVlc = new LibVLC(libVlcOptions.ToArray());
            LibVlc.Log += LibVlcOnLog;
        }

        private void LibVlcOnLog(object? sender, LogEventArgs e)
        {
            switch (e.Level)
            {
                case LogLevel.Debug:
                    Logger.Log(e.Message, LogType.Debug, null, e.Module, "LibVLC");
                    break;
                case LogLevel.Notice:
                    Logger.Log(e.Message, LogType.Info, null, e.Module, "LibVLC");
                    break;
                case LogLevel.Warning:
                    Logger.Log(e.Message, LogType.Warning, null, e.Module, "LibVLC");
                    break;
                case LogLevel.Error:
                    Logger.Log(e.Message, LogType.Error, null, e.Module, "LibVLC");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}