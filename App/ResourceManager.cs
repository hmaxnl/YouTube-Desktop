using System;
using System.Collections.Specialized;
using App.ResourceManagement;
using Avalonia.Controls;
using Avalonia.Metadata;

namespace App
{
    public sealed partial class ResourceManager
    {
        public ResourceManager()
        {
            if (Instance == null)
                Instance = this;
            else
                throw new Exception("A resource manager instance is already setup!");
            ResourceGroups.CollectionChanged += ResourceGroupsOnCollectionChanged;
        }

        private static ResourceManager Instance { get; set; } = null!;

        private static readonly IResourceDictionary ResourceDictionary = new ResourceDictionary();
        
        public static void SwitchResource(string resourceGroupId, string resourceId)
        {
            foreach (ResourceGroup group in Instance.ResourceGroups)
            {
                if (group.Identifier != resourceGroupId) continue;
                SwitchResource(group, resourceId);
            }
        }
        
        public static void SwitchResource(ResourceGroup resourceGroup, string resourceId)
        {
            if (resourceGroup == null || resourceGroup.SelectedResource?.Identifier == resourceId) return;
            foreach (Resource resource in resourceGroup.Resources)
            {
                if (resourceGroup.SelectedResource == null) return;
                if (resource.Identifier != resourceId || resourceGroup.SelectedResource.Equals(resource)) continue;
                SwitchResource(resourceGroup, resource);
            }
        }
        
        public static void SwitchResource(ResourceGroup resourceGroup, Resource resource)
        {
            if (resourceGroup.SelectedResource?.ResourceProvider == null || resource == null || !resourceGroup.Resources.Contains(resource)) return;
            ResourceDictionary.MergedDictionaries.Remove(resourceGroup.SelectedResource.ResourceProvider);
            resourceGroup.SelectedResource = resource;
            Instance.SetResourceGroup(resourceGroup);
        }
        
        private void ResourceGroupsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (ResourceGroup resourceGroup in e.NewItems)
                SetResourceGroup(resourceGroup);
        }

        private void SetResourceGroup(ResourceGroup resourceGroup)
        {
            if (resourceGroup.SelectedResource?.ResourceProvider == null) return;
            ResourceDictionary.MergedDictionaries.Add(resourceGroup.SelectedResource.ResourceProvider);
        }
    }
    public partial class ResourceManager : IResourceProvider
    {
        [Content]
        public ObservableResourceCollection<ResourceGroup> ResourceGroups { get; } =
            new ObservableResourceCollection<ResourceGroup>();

        public IResourceHost? Owner { get; }
        public event EventHandler? OwnerChanged;
        public bool HasResources => ResourceDictionary.HasResources;

        public bool TryGetResource(object key, out object? value) => ResourceDictionary.TryGetResource(key, out value);
        public void AddOwner(IResourceHost owner) => ResourceDictionary.AddOwner(owner);
        public void RemoveOwner(IResourceHost owner) => ResourceDictionary.RemoveOwner(owner);
    }
}