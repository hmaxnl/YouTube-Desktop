using System;
using System.ComponentModel;
using Avalonia.Controls;
using YouTubeGUI.View;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGuiMainBase : YouTubeGUIMain
    {
        public YouTubeGuiMainBase()
        {
            Terminal.Terminal.AppendLog("Creating main window!");
            DataContext = this;
            Closing += OnClosing;
            Opened += OnOpened;
        }

        private void OnOpened(object? sender, EventArgs e)
        {
            
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            
        }
    }
}