using System;
using YouTubeGUI.Stores;
using YouTubeGUI.ViewModels;

namespace YouTubeGUI
{
    public static class MainContentNavigator
    {
        private static ViewModelBase? _currentContentViewModel; // Default to loading!
        public static ViewModelBase CurrentContentViewModel
        {
            get => _currentContentViewModel ??= new HomeViewModel(SessionStore.DefaultSession);
            set
            {
                _currentContentViewModel?.Dispose();
                _currentContentViewModel = value;
                OnViewModelChanged();
            }
        }
        public static event Action? ViewModelChanged;
        private static void OnViewModelChanged() => ViewModelChanged?.Invoke();
    }
}