using System;
using System.Collections.Specialized;
using Avalonia.Controls;
using Avalonia.Metadata;

namespace App.ResourceManagement
{
    public class ResourceController : ControllerBase, IResourceProvider
    {
        public ResourceController()
        {
            Resources.CollectionChanged += ResourcesOnCollectionChanged;
            _resourceDictionary.OwnerChanged += OwnerChanged;
        }

        public IResourceDictionary ResourceDictionary => _resourceDictionary;
        private readonly IResourceDictionary _resourceDictionary = new ResourceDictionary();
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
                if (resource.Identifier != DefaultId) continue;
                SelectedResource = resource;
                SetResource();
            }
        }
        private void SetResource()
        {
            if (SelectedResource?.ResourceProvider == null) return;
            _resourceDictionary.MergedDictionaries.Add(SelectedResource.ResourceProvider);
        }

        [Content]
        public ObservableResourceCollection<Resource> Resources { get; } = new ObservableResourceCollection<Resource>();
        
        public bool TryGetResource(object key, out object? value) => _resourceDictionary.TryGetResource(key, out value);

        public bool HasResources => _resourceDictionary.HasResources;
        public void AddOwner(IResourceHost owner)
        {
            if (_resourceDictionary.Owner == owner) return;
            _resourceDictionary.AddOwner(owner);
        }
        public void RemoveOwner(IResourceHost owner) => _resourceDictionary.RemoveOwner(owner);

        public IResourceHost? Owner { get; }
        public event EventHandler? OwnerChanged;
    }
}