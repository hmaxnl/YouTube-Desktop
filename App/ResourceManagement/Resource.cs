using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace App.ResourceManagement
{
    public class Resource : ResourceIdBase
    {
        public Uri? ResourceUri { get; set; }
        public IResourceProvider? ResourceProvider { get; private set; }

        public void TryLoadResource(Uri? baseUri = null)
        {
            if (ResourceUri == null) return;
            ResourceProvider = (ResourceDictionary)AvaloniaXamlLoader.Load(ResourceUri, baseUri);
        }

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                null => false,
                Resource resource => resource.Identifier == Identifier,
                _ => false
            };
        }
        public override int GetHashCode() => Identifier.GetHashCode();
        public override string ToString() => $"{Identifier}_Resource";
    }
}