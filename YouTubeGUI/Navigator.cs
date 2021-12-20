using System;
using YouTubeGUI.ViewModels;

namespace YouTubeGUI
{
    public class Navigator
    {
        private ViewModelBase _currentViewModel = new LoadingViewModel(); // Default to loading!
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnViewModelChanged();
            }
        }
        public event Action? ViewModelChanged;
        public void OnViewModelChanged() => ViewModelChanged?.Invoke();
    }
}