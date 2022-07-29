using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Metadata;

namespace App.ResourceManagement
{
    public class SwitchingResource : ISwitchingResource
    {
        public SwitchingResource() => ResourceManager.AddResourceSwitch(this);
        
        [Content]
        public ObservableCollection<ResourceData> Resources { get; } = new ObservableCollection<ResourceData>();
        public string? Identifier { get; set; }
        public Uri? BaseUri { get; set; }

        public IResourceDictionary Resource => _resourceDictionary;
        public bool HasResources => Resource.HasResources;
        public event Action<SwitchingResource>? ResourceChanged;
        public bool TryGetResource(object key, out object? value) => Resource.TryGetResource(key, out value);
        private readonly ResourceDictionary _resourceDictionary = new ResourceDictionary();
        
        public IResourceHost? Owner { get; }
        public void AddOwner(IResourceHost owner) => Resource.AddOwner(owner);
        public void RemoveOwner(IResourceHost owner) => Resource.RemoveOwner(owner);
        public event EventHandler? OwnerChanged;

        private ResourceData? _selectedResource;
        public ResourceData? SelectedResource
        {
            get => _selectedResource;
            set
            {
                if (_selectedResource == value || value == null) return;
                if (_selectedResource?.Resource != null)
                    _resourceDictionary.MergedDictionaries.Remove(_selectedResource.Resource);
                _selectedResource = value;
                _selectedResource.TryLoad(null);
                SetResource();
                ResourceChanged?.Invoke(this);
            }
        }

        private void SetResource()
        {
            if (_selectedResource?.Resource != null)
                _resourceDictionary.MergedDictionaries.Add(_selectedResource.Resource);
        }

        private object? _selectedResourceId;
        public object? SelectedResourceId
        {
            get => _selectedResourceId;
            set
            {
                if (_selectedResourceId == value) return;
                _selectedResourceId = value;
                SelectedResource = Resources.FirstOrDefault(resource => resource.Identifier != null && resource.Identifier.Equals(_selectedResourceId));
            }
        }
    }
}