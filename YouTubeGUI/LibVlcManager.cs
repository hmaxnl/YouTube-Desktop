using LibVLCSharp.Shared;
using YouTubeGUI.Core;

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
            LibVlc = new LibVLC();
        }
    }
}