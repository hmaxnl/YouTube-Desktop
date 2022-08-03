using App.ResourceManagement;
using Avalonia;

namespace App
{
    public static class ControllerManager
    {
        public static void SwitchResource(string resourceGroupId, string resourceId)
        {
#pragma warning disable 8602
            foreach (var resourceProvider in Application.Current.Resources.MergedDictionaries)
#pragma warning restore 8602
            {
                if (resourceProvider is not ResourceController rc) continue;
                if (rc.Identifier != resourceGroupId) continue;
                SwitchResource(rc, resourceId);
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
            resourceController.ResourceDictionary.MergedDictionaries.Remove(resourceController.SelectedResource.ResourceProvider);
            resourceController.SelectedResource = resource;
            SetResourceGroup(resourceController);
        }

        private static void SetResourceGroup(ResourceController resourceController)
        {
            if (resourceController.SelectedResource?.ResourceProvider != null)
                resourceController.ResourceDictionary.MergedDictionaries.Add(resourceController.SelectedResource.ResourceProvider);
        }
    }
}