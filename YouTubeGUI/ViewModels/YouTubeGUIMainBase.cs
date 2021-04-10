using System;
using Avalonia.Controls;
using YouTubeGUI.GUI;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGUIMainBase : IWindowBase
    {
        public Window LinkedWindow { get => _linkedWindow; }
        private readonly Window _linkedWindow;
        
        public YouTubeGUIMainBase(Window window)
        {
            if (window != null)
                _linkedWindow = window;
            else
                throw new ArgumentNullException("Initializing window", "The provided window is not valid!");
        }

        public void Show(string title = "Window")
        {
            LinkedWindow.Title = title;
            LinkedWindow.Show();
        }
    }
}