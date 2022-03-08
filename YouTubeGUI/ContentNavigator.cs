using System;
using System.Linq;
using YouTubeGUI.Stores;
using YouTubeGUI.ViewModels;

namespace YouTubeGUI
{
    public static class ContentNavigator
    {
        private static ViewModelBase? _currentContentViewModel; // Default to loading!
        public static ViewModelBase CurrentContentViewModel
        {
            get => _currentContentViewModel ??= new HomeViewModel(WorkplaceManager.DefaultWorkspace.Sessions.First());
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