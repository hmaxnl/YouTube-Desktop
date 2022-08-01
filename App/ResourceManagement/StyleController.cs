using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Avalonia.Metadata;
using Avalonia.Styling;

namespace App.ResourceManagement
{
    public class StyleController : ControllerBase, IStyle
    {
        public StyleController()
        {
            StyleResources.CollectionChanged += StyleResourcesOnCollectionChanged;
        }

        private Resource _selectedResource;
        private void StyleResourcesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (Resource resource in e.NewItems)
            {
                resource.TryLoadResource();
                _selectedResource = resource;
                SetResource();
            }
        }

        private void SetResource()
        {
            if (_selectedResource?.Style != null)
            {
                _loaded = _selectedResource.Style;
            }
        }

        private IStyle _loaded;

        public IStyle Loaded => _loaded;
        public IReadOnlyList<IStyle> Children => _loaded?.Children ?? Array.Empty<IStyle>();
        
        [Content]
        public ObservableResourceCollection<Resource> StyleResources { get; } = new ObservableResourceCollection<Resource>();

        public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) => Loaded.TryAttach(target, host);
    }
}