using System;
using Avalonia;
using Avalonia.Controls;
using JetBrains.Annotations;
using YouTubeGUI.View;
using YouTubeScrap;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGuiMainBase : YouTubeGUIMain
    {
        public YouTubeGuiMainBase()
        {
            Terminal.Terminal.AppendLog("Creating main window!");
            DataContext = this;
        }
    }
}