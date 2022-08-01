using App.ResourceManagement;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace App
{
    public static class ControllerManager
    {
        private static readonly IResourceDictionary ResourceDictionary = new ResourceDictionary();
        private static readonly Styles Styles = new Styles();
        
        public static void SwitchResource(string resourceGroupId, string resourceId)
        {
            foreach (var resourceProvider in Application.Current.Resources.MergedDictionaries)
            {
                if (resourceProvider is ResourceController rc)
                {
                    if (rc.Identifier != resourceGroupId) continue;
                    SwitchResource(rc, resourceId);
                }
            }
        }
        
        public static void SwitchResource(ResourceController resourceController, string resourceId)
        {
            if (resourceController == null || resourceController.SelectedResource?.Identifier == resourceId) return;
            foreach (Resource resource in resourceController.Resources)
            {
                if (resourceController.SelectedResource == null) return;
                if (resource.Identifier != resourceId || resourceController.SelectedResource.Equals(resource)) continue;
                SwitchResource(resourceController, resource);
            }
        }
        
        public static void SwitchResource(ResourceController resourceController, Resource resource)
        {
            if (resourceController.SelectedResource?.ResourceProvider == null || resource == null || !resourceController.Resources.Contains(resource)) return;
            ResourceDictionary.MergedDictionaries.Remove(resourceController.SelectedResource.ResourceProvider);
            resourceController.SelectedResource = resource;
            SetResourceGroup(resourceController);
        }

        private static void SetResourceGroup(ResourceController resourceController)
        {
            if (resourceController.SelectedResource?.ResourceProvider != null)
                ResourceDictionary.MergedDictionaries.Add(resourceController.SelectedResource.ResourceProvider);
            if (resourceController.SelectedResource?.Style != null)
                Styles.Add(resourceController.SelectedResource.Style);
        }
    }
}