using YouTubeGUI.Commands;
using YouTubeGUI.Models;
using YouTubeGUI.Stores;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            MainContentNavigator.ViewModelChanged += NavigatorOnViewModelChanged;
            _session = SessionStore.DefaultSession;
            _session.MetadataChanged += SessionOnMetadataChanged;
            /* Commands */
            NavigationPaneCommand = new NavigationPaneCommand();
            NavigationPaneCommand.TogglePane += () => { IsGuidePaneOpen = !IsGuidePaneOpen; OnPropertyChanged(nameof(IsGuidePaneOpen)); };
        }

        private void SessionOnMetadataChanged()
        {
            OnPropertyChanged(nameof(Topbar));
        }

        // Properties
        private readonly UserSession _session;
        public string WindowTitle { get; set; } = "YouTube Desktop";

        public Topbar Topbar
        {
            get
            {
                if (_session.TopbarSnippet != null)
                    return _session.TopbarSnippet.Topbar;
                return null;
            }
        }

        // Current content viewmodel
        public ViewModelBase CurrentContentViewModel => MainContentNavigator.CurrentContentViewModel;
        /* Commands */
        public NavigationPaneCommand NavigationPaneCommand { get; set; }
        public bool IsGuidePaneOpen { get; set; } = true;
        
        // Privates
        private void NavigatorOnViewModelChanged() => OnPropertyChanged(nameof(CurrentContentViewModel));
        public override void Dispose() => MainContentNavigator.ViewModelChanged -= NavigatorOnViewModelChanged;
    }
}