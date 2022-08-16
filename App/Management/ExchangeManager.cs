using System.Collections.ObjectModel;
using App.ExchangeXAML;

namespace App.Management
{
    public static class ExchangeManager
    {
        public static ObservableCollection<ResourceExchanger> ResourceExchangers { get; } =
            new ObservableCollection<ResourceExchanger>();
        public static ObservableCollection<StyleExchanger> StyleExchangers { get; } =
            new ObservableCollection<StyleExchanger>();
        public static void Exchange(string exchangerId, string resourceId, bool checkAll = false)
        {
#pragma warning disable 8602
            foreach (var resourceExchanger in ResourceExchangers)
#pragma warning restore 8602
            {
                if (resourceExchanger == null || resourceExchanger.Identifier != exchangerId) continue;
                ExchangeResource(resourceExchanger, resourceId);
                if (checkAll) return;
            }

            foreach (StyleExchanger styleExchanger in StyleExchangers)
            {
                if (styleExchanger == null || styleExchanger.Identifier != exchangerId) continue;
                ExchangeStyle(styleExchanger, resourceId);
                return;
            }
        }
        public static void ExchangeResource(ResourceExchanger resourceExchanger, string resourceId)
        {
            if (resourceExchanger == null || resourceExchanger.SelectedResource?.Identifier == resourceId) return;
            foreach (ExchangeResource resource in resourceExchanger.Resources)
            {
                if (resourceExchanger.SelectedResource == null) return;
                if (resource.Identifier != resourceId || resourceExchanger.SelectedResource.Equals(resource)) continue;
                ExchangeResource(resourceExchanger, resource);
            }
        }
        public static void ExchangeResource(ResourceExchanger resourceExchanger, ExchangeResource exchangeResource)
        {
            if (resourceExchanger == null) return;
            resourceExchanger.SelectedResource = exchangeResource;
        }
        public static void ExchangeStyle(StyleExchanger styleExchanger, string resourceId)
        {
            if (styleExchanger == null) return;
            foreach (ExchangeResource styleControllerStyleResource in styleExchanger.StyleResources)
            {
                if (!styleControllerStyleResource.Identifier.Equals(resourceId)) continue;
                ExchangeStyle(styleExchanger, styleControllerStyleResource);
            }
        }
        public static void ExchangeStyle(StyleExchanger styleExchanger, ExchangeResource exchangeResource)
        {
            if (exchangeResource?.Style == null || !styleExchanger.StyleResources.Contains(exchangeResource)) return;
            styleExchanger.Exchange(exchangeResource);
        }
    }
}