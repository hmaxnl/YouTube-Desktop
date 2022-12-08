using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace App.Management
{
    public static class WindowManager
    {
        private static readonly Dictionary<string, WindowData> appWindows = new Dictionary<string, WindowData>();

        public static void Register<TWindow>(string wName, bool isMain = false) where TWindow : class
        {
            Type genType = typeof(TWindow);
            WindowData winData = new WindowData
            {
                WType = genType,
                IsMain = isMain
            };
            appWindows.Add(wName, winData);
        }

        // Construct the window class only when its needed!
        public static Window GetWindow(string wName)
        {
            if (!appWindows.TryGetValue(wName, out WindowData data))
                throw new Exception($"Could not get window: {wName}");
            data.Window = (Window?)Activator.CreateInstance(data.WType);
            appWindows[wName] = data;
            return data.Window ?? throw new Exception($"Could not construct window: {wName}");
        }
    }

    internal struct WindowData
    {
        public Window? Window;
        public bool IsMain;
        public Type WType;
    }
}