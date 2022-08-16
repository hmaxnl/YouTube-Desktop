using System;
using System.Collections.Specialized;
using App.Management;
using Avalonia.Controls;
using Avalonia.Metadata;

namespace App.ExchangeXAML
{
    public class ResourceExchanger : IdentifierBase, IResourceProvider
    {
        public ResourceExchanger()
        {
            Resources.CollectionChanged += ResourcesOnCollectionChanged;
            _resourceDictionary.OwnerChanged += OwnerChanged;
            ExchangeManager.ResourceExchangers.Add(this);
        }

        public IResourceDictionary ResourceDictionary => _resourceDictionary;
        private readonly IResourceDictionary _resourceDictionary = new ResourceDictionary();
        public string DefaultId { get; set; } = string.Empty;
        [Content]
        public ObservableResourceCollection<ExchangeResource> Resources { get; } = new ObservableResourceCollection<ExchangeResource>();
        
        public bool TryGetResource(object key, out object? value) => _resourceDictionary.TryGetResource(key, out value);

        public bool HasResources => _resourceDictionary.HasResources;
        public void AddOwner(IResourceHost owner)
        {
            if (_resourceDictionary.Owner == owner) return;
            _resourceDictionary.AddOwner(owner);
        }
        public void RemoveOwner(IResourceHost owner) => _resourceDictionary.RemoveOwner(owner);

        public IResourceHost? Owner => _resourceDictionary.Owner;
        public event EventHandler? OwnerChanged;

        private ExchangeResource? _selectedResource;
        public ExchangeResource? SelectedResource
        {
            get => _selectedResource;
            set
            {
                if (value?.ResourceProvider == null || !Resources.Contains(value)) return;// Check for null & if the resource is from this controller.
                if (_selectedResource != null && _selectedResource.Equals(value)) return;
                if (_selectedResource?.ResourceProvider != null && _resourceDictionary.MergedDictionaries.Contains(_selectedResource.ResourceProvider))
                    _resourceDictionary.MergedDictionaries.Remove(_selectedResource.ResourceProvider);
                _selectedResource = value;
                _resourceDictionary.MergedDictionaries.Add(_selectedResource.ResourceProvider);
            }
        }

        private void ResourcesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (ExchangeResource resource in e.NewItems)
            {
                resource.TryLoadResource();
                if (!resource.Identifier.Equals(DefaultId)) continue;
                SelectedResource = resource;
            }
        }
    }
}