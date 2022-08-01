using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace App.ResourceManagement
{
    public class Resource : ControllerBase
    {
        public Uri? ResourceUri { get; set; }
        public Uri? StyleUri { get; set; }
        public IResourceProvider? ResourceProvider { get; private set; }
        public Styles? Style { get; private set; }

        public void TryLoadResource(Uri? baseUri = null)
        {
            if (ResourceUri != null)
                ResourceProvider = (ResourceDictionary)AvaloniaXamlLoader.Load(ResourceUri, baseUri);
            if (StyleUri == null) return;
            Style = (Styles)AvaloniaXamlLoader.Load(StyleUri, baseUri);
        }
    }
}