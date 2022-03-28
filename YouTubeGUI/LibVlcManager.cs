using System;
using System.Collections;
using System.Collections.Generic;
using LibVLCSharp.Shared;
using Serilog;
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
            Log.Information("Initializing LibVLC...");
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
                    Log.Debug(e.Message);
                    break;
                case LogLevel.Notice:
                    Log.Information(e.Message);
                    break;
                case LogLevel.Warning:
                    Log.Warning(e.Message);
                    break;
                case LogLevel.Error:
                    Log.Error(e.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}