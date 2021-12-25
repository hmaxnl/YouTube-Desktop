using System;
using YouTubeGUI.Stores;
using YouTubeGUI.ViewModels;

namespace YouTubeGUI
{
    public static class MainContentNavigator
    {
        private static ViewModelBase _currentContentViewModel = new HomeViewModel(YoutubeUserStore.CurrentUser); // Default to loading!
        public static ViewModelBase CurrentContentViewModel
        {
            get => _currentContentViewModel;
            set
            {
                _currentContentViewModel.Dispose();
                _currentContentViewModel = value;
                OnViewModelChanged();
            }
        }
        public static event Action? ViewModelChanged;
        private static void OnViewModelChanged() => ViewModelChanged?.Invoke();
    }
}