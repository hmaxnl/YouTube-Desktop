using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Avalonia.Metadata;
using Avalonia.Styling;

namespace App.ResourceManagement
{
    public class StyleController : ControllerBase, IStyle
    {
#pragma warning disable 8618
        public StyleController()
#pragma warning restore 8618
        {
            StyleResources.CollectionChanged += StyleResourcesOnCollectionChanged;
        }

        private Resource? _selectedResource;
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
                Loaded = _selectedResource.Style;
            }
        }

        public IStyle Loaded { get; private set; }

        public IReadOnlyList<IStyle> Children => Loaded?.Children ?? Array.Empty<IStyle>();
        
        [Content]
        public ObservableResourceCollection<Resource> StyleResources { get; } = new ObservableResourceCollection<Resource>();

        public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) => Loaded.TryAttach(target, host);
    }
}