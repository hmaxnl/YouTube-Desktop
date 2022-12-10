using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace App.Management
{
    /// <summary>
    /// Simple window management, for keeping track of the windows in the application.
    /// </summary>
    public static class WindowManager
    {
        private static readonly Dictionary<string, WindowData> appWindows = new Dictionary<string, WindowData>();
        private static string? _mainWindowName;
        public static Window GetMainWindow => string.IsNullOrEmpty(_mainWindowName) ? throw new Exception("Could not get the main window!") : GetWindow(_mainWindowName);

        public static void Register<TWindow>(string wName, bool isMain = false) where TWindow : class
        {
            if (isMain)
                _mainWindowName = wName;
            WindowData winData = new WindowData
            {
                Instance = typeof(TWindow)
            };
            appWindows.Add(wName, winData);
        }
        
        public static Window GetWindow(string wName)
        {
            if (!appWindows.TryGetValue(wName, out WindowData data))
                throw new Exception($"Could not get window: {wName}");
            // Construct the window class only when its needed!
            data.Window = (Window?)Activator.CreateInstance(data.Instance);
            appWindows[wName] = data;
            return data.Window ?? throw new Exception($"Could not construct window: {wName}");
        }

        public static void RemoveWindow(string wName)
        {
            if (_mainWindowName == wName)
                _mainWindowName = string.Empty;
            appWindows.Remove(wName);
        }
    }

    internal struct WindowData
    {
        public Window? Window;
        public Type Instance;
    }
}