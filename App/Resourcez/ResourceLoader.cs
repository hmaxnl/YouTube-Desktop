using System;
using Avalonia.Controls;

namespace App.Resourcez
{
    public class ResourceLoader : IResourceProvider
    {
        public bool TryGetResource(object key, out object? value) => Resource.TryGetResource(key, out value);

        public bool HasResources => Resource.HasResources;
        public void AddOwner(IResourceHost owner) => Resource.AddOwner(owner);
        public void RemoveOwner(IResourceHost owner) => Resource.RemoveOwner(owner);

        public IResourceHost? Owner { get; }
        public event EventHandler? OwnerChanged;

        /// <summary>
        /// The resource dictionary used to store the resources.
        /// </summary>
        public IResourceDictionary Resource => _resourceDictionary;
        private ResourceDictionary _resourceDictionary = new ResourceDictionary();
    }
}