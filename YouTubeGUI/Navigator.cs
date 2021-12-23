using System;
using YouTubeGUI.ViewModels;

namespace YouTubeGUI
{
    public static class Navigator
    {
        private static ViewModelBase _currentContentViewModel = new LoadingViewModel(); // Default to loading!
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