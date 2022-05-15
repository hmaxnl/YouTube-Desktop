using System.Linq;
using YouTubeGUI.Commands;
using YouTubeGUI.Models;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Main view model for the application.
        /// </summary>
        /// <param name="workspace">The workspace the view model will use.</param>
        public MainViewModel(Workspace workspace)
        {
            //TODO (ddp): Session selection needs to be improved!
            Session = workspace.Sessions.First();
            Session.Initialized += SessionInitialized;
            ContentNavigator.ViewModelChanged += NavigatorOnViewModelChanged;
            /* Commands */
            TopbarButtonCommand = new TopbarButtonCommand(Session);
            NavigationPaneCommand = new NavigationPaneCommand();
            NavigationPaneCommand.TogglePane += () => { IsGuidePaneOpen = !IsGuidePaneOpen; };
        }

        /* Properties */
        
        /// <summary>
        /// The session this view model is using!
        /// </summary>
        public Session Session { get; }

        /// <summary>
        /// The title the linked view has.
        /// </summary>
        public string WindowTitle { get; set; } = "YouTube Desktop";

        public Topbar Topbar => Session.TopbarSnippet != null ? Session.TopbarSnippet.Topbar : new Topbar();

        // Current content viewmodel
        public ViewModelBase CurrentContentViewModel => ContentNavigator.CurrentContentViewModel;
        /* Commands */
        public TopbarButtonCommand TopbarButtonCommand { get; }
        public NavigationPaneCommand NavigationPaneCommand { get; }
        public bool IsGuidePaneOpen
        {
            get => _isGuidePaneOpen;
            set
            {
                _isGuidePaneOpen = value;
                OnPropertyChanged();
            } 
        }
        private bool _isGuidePaneOpen = true;
        
        /* Private */
        private void NavigatorOnViewModelChanged() => OnPropertyChanged(nameof(CurrentContentViewModel));

        public override void Dispose()
        {
            Session.Initialized -= SessionInitialized;
            ContentNavigator.ViewModelChanged -= NavigatorOnViewModelChanged;
        }
        private void SessionInitialized()
        {
            OnPropertyChanged(nameof(Topbar));
        }
    }
}