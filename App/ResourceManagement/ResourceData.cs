using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace App.ResourceManagement
{
    public class ResourceData
    {
        public object? Identifier { get; set; }
        public Uri ResourceUri { get; set; }

        internal IResourceProvider Resource { get; set; }

        internal void TryLoad(Uri baseUri)
        {
            if (baseUri == null) return;
            if (ResourceUri != null) Resource = (ResourceDictionary)AvaloniaXamlLoader.Load(ResourceUri, baseUri);
        }
    }
}