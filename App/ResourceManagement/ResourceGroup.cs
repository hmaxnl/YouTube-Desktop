using System.Collections.Specialized;
using Avalonia.Controls;
using Avalonia.Metadata;

namespace App.ResourceManagement
{
    public class ResourceGroup : ResourceIdBase
    {
        public ResourceGroup() => Resources.CollectionChanged += ResourcesOnCollectionChanged;

        public string DefaultId { get; set; } = string.Empty;
        
        private Resource? _selectedResource;
        public Resource? SelectedResource
        {
            get => _selectedResource;
            set
            {
                if (_selectedResource != null && _selectedResource.Equals(value)) return;
                _selectedResource = value;
            }
        }

        private void ResourcesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (Resource resource in e.NewItems)
            {
                resource.TryLoadResource();
                if (resource.Identifier == DefaultId)
                    SelectedResource = resource;
            }
        }

        [Content]
        public ObservableResourceCollection<Resource> Resources { get; } = new ObservableResourceCollection<Resource>();

        public IResourceDictionary Resource => _resourceDictionary;
        private readonly ResourceDictionary _resourceDictionary = new ResourceDictionary();

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                null => false,
                ResourceGroup rg => rg.Identifier == Identifier,
                _ => false
            };
        }

        public override int GetHashCode() => Identifier.GetHashCode();
        public override string ToString() => $"{Identifier}_ResourceGroup";
    }
}