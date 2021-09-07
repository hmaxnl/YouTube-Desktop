using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using AvaloniaEdit;
using YouTubeGUI.View;

using YouTubeScrap;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;

namespace YouTubeGUI.ViewModels
{
    public class YouTubeGuiDebugBase : YouTubeGUIDebug
    {
        YouTubeLoginBase loginWindow = new YouTubeLoginBase();
        public YouTubeGuiDebugBase()
        {
            DataContext = this;
            TextEditor debugTextEditor = this.Find<TextEditor>("DebugTextEditor");
            Terminal.Terminal.TextEditor = debugTextEditor;
        }

        public void DebugCommandTest()
        {
            Terminal.Terminal.AppendLog("Debug Command Pressed!");
            //UserCookies uCookies = CefManager.GetCookies();
            //YoutubeUser user = new YoutubeUser(uCookies);
            //user.SaveCookies();
            SettingsManager.SaveSettings();
            //loginWindow.PushLogin(ApiDataManager.InnertubeData.LoginUrl);
            //loginWindow.PushLogin("https://useragent.openadmintools.com/");
        }
    }
}