using App.Models;
using ReactiveUI;

namespace App.ViewModels
{
    public class ContentModelBase : ReactiveObject, IActivatableViewModel, IHeaderModel
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
        public string Title { get; set; } = string.Empty;
        public bool IsPlaying { get; set; }
    }
}