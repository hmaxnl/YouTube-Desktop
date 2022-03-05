using System.Collections.Generic;
using System.Linq;
using YouTubeGUI.Commands;
using YouTubeGUI.Models;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(UserSession session)
        {
            _session = session;
            _session.MetadataChanged += NotifyAllContents;
            ElementPreparedCommand = new ElementPreparedCommand();
            ElementPreparedCommand.ExecuteLoadContinuation += ExecuteOnLoadContinuation;
            //NotifyAllContents();
        }

        public ElementPreparedCommand ElementPreparedCommand { get; }
        
        public List<object?>? ContentList => _session.HomeSnippet?.Contents;
        public List<object>? GuideList => _session.GuideSnippet?.GuideItems.ToList();

        // Functions
        public override void Dispose()
        {
            ElementPreparedCommand.ExecuteLoadContinuation -= ExecuteOnLoadContinuation;
        }

        // Privates
        private readonly UserSession _session;

        private void NotifyAllContents()
        {
            OnPropertyChanged(nameof(ContentList));
            OnPropertyChanged(nameof(GuideList));
        }

        private void ExecuteOnLoadContinuation(ContinuationItemRenderer cir) => _session.HomeSnippet?.LoadContinuation(_session.SessionUser, cir);
    }
}